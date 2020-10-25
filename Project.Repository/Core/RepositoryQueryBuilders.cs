using Microsoft.EntityFrameworkCore;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Project.Repository.Core
{
    public partial class Repository<TEntity>
    {
        private IEnumerable<string[]> GetPropertiesFromFieldString(string requiredFieldsString)
        {
            return requiredFieldsString.Split(',').Select(nestedProperty => nestedProperty.Trim()).Select(property => property.Split('.'));
        }

        /// <summary>
        /// Created a select query statement by the provided fields
        /// </summary>
        /// <param name="query"></param>
        /// <param name="requiredFieldsString"></param>
        public void InitializeQueryDataShaping(ref IQueryable<TEntity> query, string requiredFieldsString)
        {
            if (!String.IsNullOrEmpty(requiredFieldsString))
            {
                var type = typeof(TEntity);

                IEnumerable<string[]> fields = GetPropertiesFromFieldString(requiredFieldsString);

                ParameterExpression parameter = Expression.Parameter(type, "s");

                var body = DynamicSelectExpression(type, parameter, fields);

                query = query.Select(Expression.Lambda<Func<TEntity, TEntity>>(body, parameter));
            }
        }

        private Expression DynamicSelectExpression(Type type, Expression source, IEnumerable<string[]> fields, int depth = 0)
        {
            var bindings = new List<MemberBinding>();

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
                        bindings.Add(Expression.Bind(targetMember, sourceMember));
                    }
                }
            }

            return Expression.MemberInit(Expression.New(type), bindings);
        }

        /// <summary>
        /// Add includes for fields provided, if no fields are provided includes all fields
        /// </summary>
        /// <param name="query"></param>
        /// <param name="fieldsString"></param>
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

        private void InitializeIncludeAll(ref IQueryable<TEntity> query)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.GetInterfaces().Contains(typeof(IBaseEntity)))
                {
                    var nestedProperties = propertyType.GetProperties();

                    foreach (var nestedProperty in nestedProperties)
                    {
                        var nestedPropertyType = nestedProperty.PropertyType;
                        if (nestedPropertyType.GetInterfaces().Contains(typeof(IBaseEntity)))
                        {
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

        private void InitializeIncludeForSelectedFields(ref IQueryable<TEntity> query, string[] fields)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            List<string> requiredTypes = new List<string>();

            foreach (var field in fields)
            {
                //TODO: check for performance improvements

                if (field.Contains("."))
                {
                    string[] nestedField = field.Split('.', 2);

                    var property = properties.FirstOrDefault(property =>
                           property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                           &&
                           property.Name.Equals(nestedField[0].Trim(), StringComparison.OrdinalIgnoreCase));

                    var nestedProperties = property.PropertyType.GetProperties();

                    var nestedProperty = nestedProperties.FirstOrDefault(property =>
                           property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                           &&
                           property.Name.Equals(nestedField[1].Trim(), StringComparison.OrdinalIgnoreCase));

                    if (property == null || nestedProperty == null)
                    {
                        continue;
                    }
                    else
                    {
                        query = query.Include(property.Name);
                    }
                }
                else
                {
                    var property = properties
                       .FirstOrDefault(property =>
                           property.PropertyType.GetInterfaces().Contains(typeof(IBaseEntity))
                           &&
                           property.Name.Equals(field.Trim(), StringComparison.OrdinalIgnoreCase));

                    if (property == null)
                    {
                        continue;
                    }
                    else
                    {
                        query = query.Include(property.Name);
                    }
                }
            }
        }
    }
}