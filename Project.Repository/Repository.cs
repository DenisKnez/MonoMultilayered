using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.Context;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly VehicleContext context;
        private DbSet<TEntity> dbSet;

        public Repository(VehicleContext vehicleContext)
        {
            this.context = vehicleContext;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Table => Entities;

        public virtual IQueryable<TEntity> TableAsNoTracking => Entities.AsNoTracking();


        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (dbSet == null)
                    dbSet = context.Set<TEntity>();

                return dbSet;
            }
        }

        public async virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Entities.FindAsync(id);
        }


    }
}
