using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Context;
using Project.Repository.Common;
using Project.Repository.Common.Repositories;
using System;   
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public VehicleContext DbContext { get; private set; }

        public IVehicleMakeRepository VehicleMakeRepository { get; set; }
        public IVehicleModelRepository VehicleModelRepository { get; set; }

        public UnitOfWork(VehicleContext dbContext, 
            IVehicleMakeRepository vehicleMakeRepository, IVehicleModelRepository vehicleModelRepository)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            DbContext = dbContext;


            VehicleMakeRepository = vehicleMakeRepository;
            VehicleModelRepository = vehicleModelRepository;
        }

        public virtual Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                var dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    DbContext.Set<TEntity>().Add(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                var dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    DbContext.Set<TEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            try
            {
                var dbEntityEntry = DbContext.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    DbContext.Set<TEntity>().Attach(entity);
                    DbContext.Set<TEntity>().Remove(entity);
                }
                return Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual Task<int> DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var entity = DbContext.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return Task.FromResult(0);
            }
            return DeleteAsync<TEntity>(entity);
        }


        public async Task<int> CommitAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await DbContext.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
    
}
