using Project.Common.Interfaces;
using Project.DAL.IDomainModels;
using Project.Model.Common;
using Project.Model.Common.IVehicleModelDomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common.IVehicleModelServices
{
    public interface IVehicleModelService
    {
        Task<IVehicleModelEntity> GetVehicleModelByIdAsync(Guid id);

        Task<IPage<IVehicleModel>> GetPaginatedFilteredListAsync(IPageSettings pageSettings);

        Task<bool> CreateVehicleModelAsync(IVehicleModel vehicleMake);

        Task<bool> UpdateVehicleModelAsync(IVehicleModel vehicleMake);

        Task<bool> DeleteVehicleModelAsync(Guid vehicleId);

    }
}
