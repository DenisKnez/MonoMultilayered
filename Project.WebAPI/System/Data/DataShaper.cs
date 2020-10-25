using Project.Common.System;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Project.WebAPI.System
{
    public class DataShaper<T> : IDataShaper<T> where T : class, IBaseRestModel
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Shaped the entered model to only have fields specified in the fields parameter
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public object ShapeData(T model, string fields)
        {
            if (String.IsNullOrEmpty(fields))
            {
                return model;
            }
            else
            {
                var properties = GetPropertiesFromFieldString(fields);
                return ReturnRequiredProperties(model, properties);
            }
        }

        /// <summary>
        /// Shaped the entered models to only have fields specified in the fields parameter
        /// </summary>
        /// <param name="models"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public List<object> ShapeData(IEnumerable<T> models, string fields)
        {
            var shapedData = new List<object>();

            if (String.IsNullOrEmpty(fields))
            {
                shapedData.AddRange(models);
            }
            else
            {
                foreach (var model in models)
                {
                    var properties = GetPropertiesFromFieldString(fields);
                    shapedData.Add(ReturnRequiredProperties(model, properties));
                }
            }
            return shapedData;
        }

        /// <summary> Returns the items inside the list with properties that are passed in the
        /// fields parameter the rest are removed </summary> <param name="pagedList" <param
        /// name="fields"></param> <returns></returns>
        public object PaginatedShapeData(PagedList<T> pagedList, string fields)
        {
            var responseObject = new ExpandoObject() as IDictionary<string, object>;

            foreach (var property in pagedList.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                responseObject.Add(property.Name, property.GetValue(pagedList));
            }

            var data = new List<object>();

            responseObject.Add(nameof(data), data);

            if (String.IsNullOrEmpty(fields))
            {
                data.AddRange(pagedList.AsEnumerable());
            }
            else
            {
                foreach (var model in pagedList.AsEnumerable())
                {
                    var properties = GetPropertiesFromFieldString(fields);
                    data.Add(ReturnRequiredProperties(model, properties));
                }
            }
            return responseObject;
        }

        /// <summary>
        /// Takes in an object and returns that object with only the properties that are necessary
        /// </summary>
        /// <param name="value"></param>
        /// <param name="requiredProperties"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        private ExpandoObject ReturnRequiredProperties(object value, IEnumerable<string[]> requiredProperties, int depth = 0)
        {
            var shapedObject = new ExpandoObject();

            var classProperties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in requiredProperties.GroupBy(path => path[depth]))
            {
                var matchedProperty = classProperties.FirstOrDefault(prop => prop.Name.Equals(property.Key, StringComparison.InvariantCultureIgnoreCase));

                if (matchedProperty == null)
                {
                    continue;
                }
                else
                {
                    var childMembers = property.Where(path => depth + 1 < path.Length);

                    if (matchedProperty.PropertyType.BaseType.Equals(typeof(BaseRestModel)) && childMembers.Any())
                    {
                        var complexObject = ReturnRequiredProperties(matchedProperty.GetValue(value), childMembers, depth + 1);
                        shapedObject.TryAdd(matchedProperty.Name, complexObject);
                    }
                    else
                    {
                        var objectPropertyValue = matchedProperty.GetValue(value);
                        shapedObject.TryAdd(matchedProperty.Name, objectPropertyValue);
                    }
                }
            }
            return shapedObject;
        }

        /// <summary>
        /// Takes in a string with needed properties in the format where every property is separated
        /// by , and nested properties are marked as .
        /// </summary>
        /// <param name="requiredFieldsString"></param>
        /// <returns></returns>
        private IEnumerable<string[]> GetPropertiesFromFieldString(string requiredFieldsString)
        {
            return requiredFieldsString.Split(',').Select(nestedProperty => nestedProperty.Trim()).Select(property => property.Split('.'));
        }
    }
}