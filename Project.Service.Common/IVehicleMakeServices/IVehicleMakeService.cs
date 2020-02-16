using Project.Common;
using Project.Common.Interfaces;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model.Common;
using Project.Model.Common.IVehicleMakeDomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common.IVehicleMakeServices
{
    public interface IVehicleMakeService
    {
        Task<IVehicleMakeEntity> GetVehicleMakeByIdAsync(Guid id);

        Task<IPage<IVehicleMake>> GetPaginatedFilteredListAsync(IPageSettings pageSettings);

        Task<bool> CreateVehicleMakeAsync(IVehicleMake vehicleMake);

        Task<bool> UpdateVehicleMakeAsync(IVehicleMake vehicleMake);

        Task<bool> DeleteVehicleMakeAsync(Guid vehicleId);

    }
}
