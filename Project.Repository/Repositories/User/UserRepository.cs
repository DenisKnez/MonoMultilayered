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

            InitializeBaseFilter(ref query, parameters);

        }
    }
}
