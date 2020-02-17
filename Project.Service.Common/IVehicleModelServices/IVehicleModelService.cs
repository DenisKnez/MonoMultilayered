using Project.Common.Interfaces;
using Project.DAL.IDomainModels;
using Project.Model.Common;
using Project.Model.Common.IVehicleModelDomainModels;
using Project.Model.Common.IVehicleModelDomainModels.CRUD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common.IVehicleModelServices
{
    public interface IVehicleModelService
    {
        Task<IVehicleModel> GetVehicleModelByIdAsync(Guid id);

        Task<IPage<IVehicleModel>> GetPaginatedFilteredListAsync(IPageSettings pageSettings);

        Task<bool> CreateVehicleModelAsync(ICreateVehicleModel vehicleMake);

        Task<bool> UpdateVehicleModelAsync(IUpdateVehicleModel vehicleMake);

        Task<bool> DeleteVehicleModelAsync(Guid vehicleId);

    }
}
