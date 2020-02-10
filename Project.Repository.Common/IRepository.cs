using Microsoft.EntityFrameworkCore;
using Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableAsNoTracking { get; }
    }
}
