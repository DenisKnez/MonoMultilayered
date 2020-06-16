using Microsoft.EntityFrameworkCore;
using Project.Common.Application;
using Project.Common.System;
using Project.DAL;
using Project.DAL.EntityModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private IUnitOfWork UnitOfWork { get; set; }

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

        public virtual async Task<PagedList<TEntity>> FindAsyncNoTracking(Parameters parameters)
        {
            return await PagedList<TEntity>.ToPagedListAsync(UnitOfWork.Context.Set<TEntity>().AsNoTracking(), parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Parameters parameters)
        {
            return await UnitOfWork.Context.Set<TEntity>().Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsyncNoTracking(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Set<TEntity>().Where(predicate).AsAsyncEnumerable();
        }

        public virtual TEntity Update(TEntity entity)
        {
            InitializeDateUpdated(entity);
            UnitOfWork.Context.Set<TEntity>().Attach(entity);
            UnitOfWork.Context.Update(entity);
            return entity;
        }
    }
}
