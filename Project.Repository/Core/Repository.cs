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
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;


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

        public virtual async Task<IPagedList<TEntity>> FindAsyncNoTracking<TParameters>(TParameters parameters, IQueryable<TEntity> query = null) where TParameters : IParameters
        {
            if(query == null)
            {
                return await PagedList<TEntity>.ToPagedListAsync(UnitOfWork.Context.Set<TEntity>().AsNoTracking(), parameters.PageNumber, parameters.PageSize);
            }
            else
            {
                InitializeDataShaping(ref query, parameters.Fields);

                return await PagedList<TEntity>.ToPagedListAsync(query.AsNoTracking(), parameters.PageNumber, parameters.PageSize);
            }


        }

        public virtual async Task<IPagedList<TEntity>> FindAsync<TParameters>(TParameters parameters, IQueryable<TEntity> query = null) where TParameters : IParameters
        {
            if(query == null)
            {
                return await PagedList<TEntity>.ToPagedListAsync(UnitOfWork.Context.Set<TEntity>(), parameters.PageNumber, parameters.PageSize);
            }
            else
            {
                InitializeDataShaping(ref query, parameters.Fields);

                return await PagedList<TEntity>.ToPagedListAsync(query, parameters.PageNumber, parameters.PageSize);
            }

        }

        public virtual async Task<TEntity> GetAsyncNoTracking(Guid id, string fieldsString = "")
        {
            var set = UnitOfWork.Context.Set<TEntity>().AsNoTracking().AsQueryable();

            InitializeDataShaping(ref set, fieldsString);
            return await set.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id, string fieldsString = "")
        {
            var set = UnitOfWork.Context.Set<TEntity>().AsQueryable();

            InitializeDataShaping(ref set, fieldsString);
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
