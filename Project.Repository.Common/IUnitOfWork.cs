using Project.DAL;
using Project.Repository.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IVehicleMakeRepository VehicleMakeRepository { get; set; }
        IVehicleModelRepository VehicleModelRepository { get; set; }

        Task<int> CommitAsync();

        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<int> DeleteAsync<TEntity>(Guid id) where TEntity : BaseEntity;
    }
}
