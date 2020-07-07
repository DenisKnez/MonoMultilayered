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
using Project.Common.Filters;
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

        public Task<IPagedList<User>> FindUserAsync(IParameters<IUserFilter> userParameters)
        {
            IQueryable<User> query = UnitOfWork.Context.Set<User>();

            InitializeFilter(ref query, userParameters);
            InitializeSorting(ref query, userParameters.OrderBy);

            return base.FindAsync<IParameters<IUserFilter>, IUserFilter>(userParameters, query);
        }


        public void InitializeFilter(ref IQueryable<User> query, IParameters<IUserFilter> userParameters)
        {

            if (string.IsNullOrWhiteSpace(userParameters.Filter.Name))
            {
                query.Where(x => EF.Functions.Like(x.Name, $"%{userParameters.Filter.Name}%"));
            }
            if (userParameters.Filter.DateCreated != null)
            {
                query.Where(x => x.DateCreated < userParameters.Filter.DateCreated);
            }

            InitializeBaseFilter<IParameters<IUserFilter>, IUserFilter>(ref query, userParameters);

        }
    }
}
