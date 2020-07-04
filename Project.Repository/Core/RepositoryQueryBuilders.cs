using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Project.Repository.Core
{
    public partial class Repository<TEntity>
    {
        public void InitializeDataShaping(ref IQueryable<TEntity> query, string fieldsString)
        {
            if (!String.IsNullOrEmpty(fieldsString))
            {
                var type = typeof(TEntity);

                IEnumerable<string[]> fields = fieldsString.Split(',').Select(x => x.Trim()).Select(properties => properties.Split('.'));

                ParameterExpression parameter = Expression.Parameter(type, "s");

                var body = DynamicSelectExpression(type, parameter, fields);

                query = query.Select(Expression.Lambda<Func<TEntity, TEntity>>(body, parameter));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private Expression DynamicSelectExpression(Type type, Expression source, IEnumerable<string[]> fields, int depth = 0)
        {
            Debug.WriteLine("RECURSION METHOD: Depth {0}, Type: {1}, Source: {2}, SplitStringArray: {3}", depth, type, source, fields.Select(s => s[depth]));

            var bindings = new List<MemberBinding>();

            // t
            var target = Expression.Parameter(type, type.Name);


            foreach (var field in fields.GroupBy(path => path[depth]))
            {
                var classProperties = type.GetProperties();
                var matchedProperty = classProperties.FirstOrDefault(property => property.Name.Equals(field.Key, StringComparison.InvariantCultureIgnoreCase));

                if (matchedProperty == null)
                {
                    continue;
                }
                else
                {
                    // t.fieldName
                    var sourceMember = Expression.Property(source, matchedProperty);

                    var targetMember = type.GetProperty(matchedProperty.Name);

                    var childMembers = field.Where(path => depth + 1 < path.Length);

                    if (childMembers.Any() && targetMember.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity)))
                    {
                        var targetValue = DynamicSelectExpression(targetMember.PropertyType, sourceMember, childMembers, depth + 1);
                        bindings.Add(Expression.Bind(targetMember, targetValue));
                    }
                    else
                    {
                        // fieldName
                        //var targetValue = type.GetProperty(targetPropertyString);
                        // fieldName = typeName.fieldName
                        bindings.Add(Expression.Bind(targetMember, sourceMember));
                    }

                }

            }

            return Expression.MemberInit(Expression.New(type), bindings);
        }



        public void InitializeInclude(ref IQueryable<TEntity> query, string fieldsString)
        {
            if (!String.IsNullOrEmpty(fieldsString))
            {
                string[] fields = fieldsString.Split(',').Select(x => x.Trim()).ToArray();
                InitializeIncludeForSelectedFields(ref query, fields);
            }
            else
            {
                InitializeIncludeAll(ref query);
            }

        }

        private  void InitializeIncludeAll(ref IQueryable<TEntity> query)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                Debug.WriteLine("Property: " + propertyType.Name);
                if (propertyType.GetInterfaces().Contains(typeof(IBaseEntity)))
                {
                    Debug.WriteLine("Base entity propeprty: " + property.Name);

                    var nestedProperties = propertyType.GetProperties();

                    Debug.WriteLine("Type property: " + property.Name);
                    foreach (var nestedProperty in nestedProperties)
                    {

                        Debug.WriteLine("Nested propeprty: " + nestedProperty.PropertyType.Name + "name: " +  nestedProperty.Name);

                        var nestedPropertyType = nestedProperty.PropertyType;
                        if (nestedPropertyType.GetInterfaces().Contains(typeof(IBaseEntity)))
                        {
                            Debug.WriteLine("Base entity nested propeprty: " + nestedPropertyType.Name);
                            query = query.Include(property.Name + "." + nestedProperty.Name);
                        }
                        else
                        {
                            query = query.Include(property.Name);
                        }

                    }

                }
            }
        }

        public void InitializeIncludeForSelectedFields(ref IQueryable<TEntity> query, string[] fields)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            List<string> requiredTypes = new List<string>();


            foreach (var field in fields)
            {

                if (field.Contains("."))
                {
                    string[] nestedField = field.Split('.', 2);

                    var property = properties.FirstOrDefault(property =>
                           property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                           &&
                           property.Name.Equals(nestedField[0].Trim(), StringComparison.InvariantCultureIgnoreCase));


                    var nestedProperties = property.PropertyType.GetProperties();


                    var nestedProperty = nestedProperties.FirstOrDefault(property =>
                           property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                           &&
                           property.Name.Equals(nestedField[1].Trim(), StringComparison.InvariantCultureIgnoreCase));


                    if(property == null || nestedProperty == null)
                    {
                        continue;
                    }
                    else
                    {
                        query = query.Include(field);
                    }


                }
                else
                {
                     var property = properties
                        .FirstOrDefault(property =>
                            property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                            &&
                            property.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));


                    if (property == null)
                    {
                        continue;
                    }
                    else
                    {
                        query = query.Include(field);
                    }

                }




            }

        }



    }
}
