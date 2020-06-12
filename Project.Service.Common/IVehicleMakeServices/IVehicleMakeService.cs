//using Project.Common;
//using Project.Common.Interfaces;
//using Project.DAL.DomainModels;
//using Project.DAL.IDomainModels;
//using Project.Model.Common;
//using Project.Model.Common.IVehicleMakeDomainModels;
//using Project.Model.Common.IVehicleMakeDomainModels.CRUD;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Project.Service.Common.IVehicleMakeServices
//{
//    public interface IVehicleMakeService
//    {
//        Task<IVehicleMake> GetVehicleMakeByIdAsync(Guid id);

//        Task<List<IVehicleMake>> GetVehiclesAsync();

//        Task<IPage<IVehicleMake>> GetPaginatedFilteredListAsync(IPageSettings pageSettings);

//        Task<bool> CreateVehicleMakeAsync(ICreateVehicleMake vehicleMake);

//        Task<bool> UpdateVehicleMakeAsync(IUpdateVehicleMake vehicleMake);

//        Task<bool> DeleteVehicleMakeAsync(Guid vehicleId);

//    }
//}
