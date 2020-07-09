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

        public Task<IPagedList<User>> FindUserAsync(IParameters<UserFilter> userParameters)
        {
            IQueryable<User> query = UnitOfWork.Context.Set<User>();

            InitializeFilter(ref query, userParameters);
            InitializeSorting(ref query, userParameters.OrderBy);

            return base.FindAsyncNoTracking<IParameters<UserFilter>, UserFilter>(userParameters, query);
        }

        public void InitializeFilter(ref IQueryable<User> query, IParameters<UserFilter> userParameters)
        {
            if (userParameters.Filter == null)
            {
                if (!String.IsNullOrWhiteSpace(userParameters.Filter.Name))
                {
                    query = query.Where(x => EF.Functions.Like(x.Name, $"%{userParameters.Filter.Name}%"));
                }
                if (userParameters.Filter.DateCreated != null)
                {
                    query = query.Where(x => x.DateCreated < userParameters.Filter.DateCreated);
                }
            }

            InitializeBaseFilter<IParameters<UserFilter>, UserFilter>(ref query, userParameters);
        }
    }
}