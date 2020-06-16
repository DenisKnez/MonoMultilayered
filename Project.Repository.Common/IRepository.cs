using Microsoft.EntityFrameworkCore;
using Project.Common.Application;
using Project.Common.System;
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
        Task<PagedList<TEntity>> FindAsyncNoTracking(Parameters parameters);
        Task<IEnumerable<TEntity>> FindAsync(Parameters parameters);
        Task<TEntity> AddAsync(TEntity entity);
        Task Delete(Guid id);
        TEntity Update(TEntity entity);
        Task DeactivateAsync(Guid id);
    }
}
