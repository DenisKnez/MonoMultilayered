using Microsoft.EntityFrameworkCore;
using Project.Common.Application;
using Project.Common.System;
using Project.DAL;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Project.Repository
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public Task<IPagedList<UserEntity>> FindUserAsync(UserParameters parameters)
        {
            IQueryable<UserEntity> query = UnitOfWork.Context.Set<UserEntity>();

            InitializeFilter(ref query, parameters);
            InitializeSorting(ref query, parameters.OrderBy);

            return base.FindAsync(parameters, query);
        }


        public void InitializeFilter(ref IQueryable<UserEntity> query, UserParameters parameters)
        {

            if (string.IsNullOrWhiteSpace(parameters.Name))
            {
                query.Where(x => EF.Functions.Like(x.Name, $"%{parameters.Name}%"));
            }
            if (parameters.DateCreated != null)
            {
                query.Where(x => x.DateCreated < parameters.DateCreated);
            }

            base.InitializeFilter(ref query, parameters);

        }

        public new void InitializeSorting(ref IQueryable<UserEntity> query, string orderByQueryString)
        {

            //if (string.IsNullOrWhiteSpace(orderByQueryString))
            //{
            //    query = query.OrderBy(entity => entity.Name);
            //    return;
            //}

            base.InitializeSorting(ref query, orderByQueryString);
        }

        //public void  InitializeSorting(ref IQueryable<UserEntity> query, string orderByQueryString)
        //{
        //    if (!query.Any())
        //    {
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(orderByQueryString))
        //    {
        //        query = query.OrderBy(entity => entity.Name);
        //        return;
        //    }

        //    var orderParams = orderByQueryString.Trim().Split(',');
        //    var properyInfos = typeof(UserEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    var orderQueryBuilder = new StringBuilder();

        //    foreach (var param in orderParams)
        //    {
        //        if (string.IsNullOrWhiteSpace(param))
        //        {
        //            continue;
        //        }

        //        var propertyFromQueryName = param.Split(" ")[0];
        //        var objectProperty = properyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));


        //        if(objectProperty == null)
        //        {
        //            continue;
        //        }

        //        var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

        //        orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");

        //    }

        //    var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');


        //    if (string.IsNullOrWhiteSpace(orderQuery))
        //    {
        //        query = query.OrderBy(x => x.Name);
        //        return;
        //    }

        //    query = query.OrderBy(orderQuery);

        //}



    }
}
