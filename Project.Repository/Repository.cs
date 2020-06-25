using Microsoft.EntityFrameworkCore;
using Project.Common.System;
using Project.DAL;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Project.Common;

namespace Project.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            Initialize(entity);
            await UnitOfWork.Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        #region Entity Setup Methods

        protected void Initialize(TEntity entity)
        {
            entity.IsActive = true;
            entity.DateUpdated = DateTime.Now;
            InitializeDateUpdated(entity);
        }

        protected void InitializeDateUpdated(TEntity entity)
        {
            entity.DateUpdated = DateTime.UtcNow;
        }

        #endregion


        public virtual async Task DeactivateAsync(Guid id)
        {
            var entity = await UnitOfWork.Context.Set<TEntity>().FindAsync(id);

            entity.IsActive = false;

            if (entity != null)
            {
                UnitOfWork.Context.Set<TEntity>().Attach(entity);
                UnitOfWork.Context.Update(entity);
            }
        }

        public virtual async Task Delete(Guid id)
        {
            TEntity entity = await UnitOfWork.Context.Set<TEntity>().FindAsync(id);
            if (entity != null) UnitOfWork.Context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<IPagedList<TEntity>> FindAsyncNoTracking<TParameters>(TParameters parameters) where TParameters : IParameters
        {
            return await PagedList<TEntity>.ToPagedListAsync(UnitOfWork.Context.Set<TEntity>().AsNoTracking(), parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task<IPagedList<TEntity>> FindAsync<TParameters>(TParameters parameters, IQueryable<TEntity> source = null) where TParameters : IParameters
        {
            if(source == null)
            {
                return await PagedList<TEntity>.ToPagedListAsync(UnitOfWork.Context.Set<TEntity>(), parameters.PageNumber, parameters.PageSize);
            }
            else
            {
                return await PagedList<TEntity>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            }

        }

        public virtual async Task<TEntity> GetAsyncNoTracking(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        //public virtual IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return UnitOfWork.Context.Set<TEntity>().Where(predicate).AsAsyncEnumerable();
        //}

        public virtual TEntity Update(TEntity entity)
        {
            InitializeDateUpdated(entity);
            UnitOfWork.Context.Set<TEntity>().Attach(entity);
            UnitOfWork.Context.Update(entity);
            return entity;
        }

        public void InitializeBaseFilter<TParameters>(ref IQueryable<TEntity> query, TParameters parameters) where TParameters : IParameters
        {
            if(parameters.IsActive)
            {
                query.Where(entity => entity.IsActive == true);
            }
        }

        /// <summary>
        /// extract sort parameters from sort query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderByQueryString"></param>
        /// <param name="defaultSort">If default sort is true sorting will be done by id, use this when you don't implement default sort</param>
        public virtual void InitializeSorting(ref IQueryable<TEntity> query, string orderByQueryString)
        {
            if (!query.Any())
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(orderByQueryString))
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



    }
}
