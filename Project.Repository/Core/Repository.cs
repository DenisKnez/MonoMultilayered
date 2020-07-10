using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.System;
using Project.DAL;
using Project.Repository.Common;
using Project.Repository.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository.Core
{
    public abstract partial class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
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

        #endregion Entity Setup Methods

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

        public virtual async Task<IPagedList<TEntity>> FindAsyncNoTracking<TIParameters, TFilter>(TIParameters parameters, IQueryable<TEntity> query = null) where TIParameters : IParameters<TFilter> where TFilter : IBaseFilter
        {
            if (query == null)
            {
                query = UnitOfWork.Context.Set<TEntity>();
            }

            InitializeInclude(ref query, parameters.Fields);
            InitializeQueryDataShaping(ref query, parameters.Fields);
            InitializeIBaseFilter<TIParameters, TFilter>(ref query, parameters);

            Debug.WriteLine("Find with no tracking: " + query.ToSql());

            return await PagedList<TEntity>.ToPagedListAsync(query.AsNoTracking(), parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task<IPagedList<TEntity>> FindAsync<TIParameters, TFilter>(TIParameters parameters, IQueryable<TEntity> query = null) where TIParameters : IParameters<TFilter> where TFilter : IBaseFilter
        {
            if (query == null)
            {
                query = UnitOfWork.Context.Set<TEntity>();
            }

            InitializeInclude(ref query, parameters.Fields);
            InitializeQueryDataShaping(ref query, parameters.Fields);
            InitializeIBaseFilter<TIParameters, TFilter>(ref query, parameters);

            return await PagedList<TEntity>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
        }

        public virtual async Task<TEntity> GetAsyncNoTracking(Guid id, string fieldsString = "")
        {
            var set = UnitOfWork.Context.Set<TEntity>().AsNoTracking().AsQueryable();

            InitializeQueryDataShaping(ref set, fieldsString);
            return await set.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id, string fieldsString = "")
        {
            var set = UnitOfWork.Context.Set<TEntity>().AsQueryable();

            InitializeQueryDataShaping(ref set, fieldsString);
            return await set.SingleOrDefaultAsync(x => x.Id == id);
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