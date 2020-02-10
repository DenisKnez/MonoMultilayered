﻿using Project.DAL.Context;
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

        Task<int> CommitAsync();

        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(Guid id) where TEntity : class;
    }
}
