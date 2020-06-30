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
                string[] fields = fieldsString.Split(',').Select(x => x.Trim()).ToArray();
                var m = DynamicSelectExpression(fields);
                Debug.WriteLine("SELECT STUFF: " + m.Body.ToString());
                query = query.Select(DynamicSelectExpression(fields));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        private Expression<Func<TEntity, TEntity>> DynamicSelectExpression(string[] fields)
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(TEntity), "o");

            // new statement "new TEntity()"
            var xNew = Expression.New(typeof(TEntity));

            List<MemberBinding> memberBindings = new List<MemberBinding>();

            foreach (var field in fields)
            {
                if (field.Contains("."))
                {
                    string[] m = field.Split('.', 2);

                    var nestedTypes = typeof(TEntity).GetProperties();

                    var nestedType = nestedTypes.FirstOrDefault(x => x.Name.Equals(m[0].Trim(), StringComparison.InvariantCultureIgnoreCase));

                    var newNestedType = Expression.New(nestedType.GetType());

                    var newParameter = Expression.Parameter(nestedType.GetType(), "p");


                    var xProperty = Expression.Property(newParameter, nestedType);



                    // set value "Field1 = o.Field1"
                    memberBindings.Add(Expression.Bind(nestedType, newNestedType));


                    //// initialization "new TEntity { Field1 = o.Field1, Field2 = o.Field2 }"
                    //var xInit = Expression.MemberInit(xNew, memberBindings);



                    //// expression "o => new TEntity { Field1 = o.Field1, Field2 = o.Field2 }"
                    //var lambda = Expression.Lambda<Func<TEntity, TEntity>>(xInit, xParameter);


                }



                var property = properties.FirstOrDefault(x => x.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                if(property == null)
                {
                    continue;
                }
                else
                {
                    // property "Field1"
                    //var mi = typeof(TEntity).GetProperty(property);

                    // original value "o.Field1"
                    var xOriginal = Expression.Property(xParameter, property);

                    // set value "Field1 = o.Field1"
                    memberBindings.Add(Expression.Bind(property, xOriginal));
                }

            }

            // initialization "new TEntity { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, memberBindings);

            // expression "o => new TEntity { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<TEntity, TEntity>>(xInit, xParameter);

            // compile to Func<TEntity, TEntity>
            return lambda;
        }



        public void FirstInitializeInclude(ref IQueryable<TEntity> query, string fieldsString)
        {
            if (!String.IsNullOrEmpty(fieldsString))
            {
                string[] fields = fieldsString.Split(',').Select(x => x.Trim()).ToArray();
                InitializeInclude(ref query, fields);
            }

        }

        public void InitializeInclude(ref IQueryable<TEntity> query, string[] fields)
        {

            Type[] propertiesTypes = typeof(TEntity).GetNestedTypes();

            List<string> requiredTypes = new List<string>();

            foreach (var field in fields)
            {
                if (field.Contains('.'))
                {
                    string[] m = field.Split('.', 2);

                    foreach (var typeProperty in propertiesTypes)
                    {

                        var nestedProperty = propertiesTypes.FirstOrDefault(x => x.Name.Equals(m[0].Trim(), StringComparison.InvariantCultureIgnoreCase));

                        Type[] properties = typeProperty.GetNestedTypes();

                        var nestednestedProperty = properties.FirstOrDefault(x => x.Name.Equals(m[1].Trim(), StringComparison.InvariantCultureIgnoreCase));

                        if (nestedProperty == null || nestednestedProperty == null)
                        {
                            continue;
                        }
                        else
                        {
                            requiredTypes.Add(field);    
                        }
                    }

                }
                else
                {
                    var property = propertiesTypes.FirstOrDefault(x => x.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                    {
                        continue;
                    }
                    else
                    {
                        requiredTypes.Add(field);
                    }
                }

            }

            foreach (var requiredType in requiredTypes)
            {
                query = query.Include(requiredType);
            }


        }



    }
}
