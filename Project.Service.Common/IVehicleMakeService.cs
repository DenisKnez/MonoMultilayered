using Project.Common;
using Project.Common.Interfaces;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IVehicleMake> GetVehicleMakeByIdAsync(Guid id);

        Task<IPage<IVehicleMake>> GetPaginatedFilteredListAsync(IPageSettings pageSettings);

        Task<bool> CreateVehicleMakeAsync(IVehicleMake vehicleMake);

        Task<bool> UpdateVehicleMakeAsync(IVehicleMake vehicleMake);

        Task<bool> DeleteVehicleMakeAsync(Guid vehicleId);

    }
}
