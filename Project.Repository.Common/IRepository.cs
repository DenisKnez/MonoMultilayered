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
        Task<TEntity> GetAsync(Guid id);
        IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        Task Delete(TEntity entity);
        void Update(TEntity entity);

    }
}
