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
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsyncNoTracking(Guid id);
        IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        Task Delete(Guid id);
        TEntity Update(TEntity entity);
        Task DeactivateAsync(Guid id);
    }
}
