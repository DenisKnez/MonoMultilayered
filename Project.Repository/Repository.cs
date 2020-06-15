using Microsoft.EntityFrameworkCore;
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

        private readonly string IsActivePropertyName = "IsActive";

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            //DbSet = unitOfWork.Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Initialize(entity);
            await UnitOfWork.Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

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

        public async Task DeactivateAsync(Guid id)
        {
            var entity = await UnitOfWork.Context.Set<TEntity>().FindAsync(id);

            entity.IsActive = false;

            if (entity != null)
            {
                UnitOfWork.Context.Set<TEntity>().Attach(entity);
                UnitOfWork.Context.Update(entity);
            }
        }

        public async Task Delete(Guid id)
        {
            TEntity entity = await UnitOfWork.Context.Set<TEntity>().FindAsync(id);
            if (entity != null) UnitOfWork.Context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsyncNoTracking(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Set<TEntity>().Where(predicate).AsAsyncEnumerable();
        }

        public TEntity Update(TEntity entity)
        {
            InitializeDateUpdated(entity);
            UnitOfWork.Context.Set<TEntity>().Attach(entity);
            UnitOfWork.Context.Update(entity);
            return entity;
        }
    }
}
