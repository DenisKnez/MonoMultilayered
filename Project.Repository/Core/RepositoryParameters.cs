using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Project.Common;

namespace Project.Repository.Core
{
    public partial class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// extract sort parameters from order by query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderByQueryString"></param>
        /// <param name="defaultSort">If default sort is true sorting will be done by id, use this when you don't implement default sort</param>
        public virtual void InitializeSorting(ref IQueryable<TEntity> query, string orderByQueryString)
        {
            if (!query.Any() || string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var properyInfos = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = properyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));


                if (objectProperty == null)
                {
                    continue;
                }

                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");

            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            query = query.OrderBy(orderQuery);

        }


        public void InitializeBaseFilter<TParameters>(ref IQueryable<TEntity> query, TParameters parameters) where TParameters : IParameters
        {
            if (parameters.IsActive)
            {
                query = query.Where(entity => entity.IsActive == true);
            }
        }


    }
}
