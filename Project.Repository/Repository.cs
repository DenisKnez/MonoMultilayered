using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private IUnitOfWork UnitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            //DbSet = unitOfWork.Context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            Initialize(entity);
            await UnitOfWork.Context.Set<TEntity>().AddAsync(entity);
        }

        public void Initialize(TEntity entity)
        {
            entity.IsActive = true;
        }

        public async Task Delete(TEntity entity)
        {
            TEntity existing = await UnitOfWork.Context.Set<TEntity>().FindAsync(entity);
            if (existing != null) UnitOfWork.Context.Set<TEntity>().Remove(existing);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await UnitOfWork.Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Set<TEntity>().Where(predicate).AsAsyncEnumerable();
        }

        public void Update(TEntity entity)
        {
            UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            UnitOfWork.Context.Set<TEntity>().Attach(entity);
        }
    }
}
