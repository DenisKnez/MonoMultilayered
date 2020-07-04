using Microsoft.EntityFrameworkCore;
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
using Project.Common;
using Project.Repository.Core;
using Project.Repository.Extensions;
using System.Diagnostics;

namespace Project.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public Task<IPagedList<User>> FindUserAsync(IUserParameters userParameters)
        {
            IQueryable<User> query = UnitOfWork.Context.Set<User>();

            InitializeFilter(ref query, userParameters);
            InitializeSorting(ref query, userParameters.OrderBy);

            return base.FindAsyncNoTracking(userParameters, query);
        }


        public void InitializeFilter(ref IQueryable<User> query, IUserParameters userParameters)
        {

            if (string.IsNullOrWhiteSpace(userParameters.Name))
            {
                query.Where(x => EF.Functions.Like(x.Name, $"%{userParameters.Name}%"));
            }
            if (userParameters.DateCreated != null)
            {
                query.Where(x => x.DateCreated < userParameters.DateCreated);
            }

            InitializeBaseFilter(ref query, userParameters);

        }
    }
}
