using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Filters;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using Project.Repository.Core;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Task<IPagedList<User>> FindUserAsync(Parameters<UserFilter> userIParameters)
        {
            IQueryable<User> query = UnitOfWork.Context.Set<User>();

            InitializeFilter(ref query, userIParameters);
            InitializeSorting(ref query, userIParameters.OrderBy);

            return base.FindAsyncNoTracking<Parameters<UserFilter>, UserFilter>(userIParameters, query);
        }

        public void InitializeFilter(ref IQueryable<User> query, Parameters<UserFilter> userIParameters)
        {
            if (userIParameters.Filter != null)
            {
                if (!String.IsNullOrWhiteSpace(userIParameters.Filter.Name))
                {
                    query = query.Where(user => EF.Functions.Like(user.Name, $"%{userIParameters.Filter.Name}%"));
                }
                if (userIParameters.Filter.DateCreated != null)
                {
                    query = query.Where(x => x.DateCreated < userIParameters.Filter.DateCreated);
                }
            }

            InitializeIBaseFilter<Parameters<UserFilter>, UserFilter>(ref query, userIParameters);
        }
    }
}