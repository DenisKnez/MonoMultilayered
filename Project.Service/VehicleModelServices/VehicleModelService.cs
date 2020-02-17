using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Common.Interfaces;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model;
using Project.Model.Common;
using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Model.Common.IVehicleModelDomainModels;
using Project.Model.Common.IVehicleModelDomainModels.CRUD;
using Project.Model.VehicleModelDomainModels;
using Project.Repository.Common;
using Project.Service.Common.IVehicleModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.VehicleModelServices
{
    public class VehicleModelService : IVehicleModelService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; } 


        public VehicleModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }


        public async Task<IPage<IVehicleModel>> GetPaginatedFilteredListAsync(IPageSettings pageSettings)
        {
            IQueryable<VehicleModelEntity> tableQuery = UnitOfWork.VehicleModelRepository.TableAsNoTracking;

            if (!String.IsNullOrEmpty(pageSettings.SearchString))
            {
                tableQuery = tableQuery.Where(x => x.VehicleMakeEntity.Name.Contains(pageSettings.SearchString));
            }


            switch (pageSettings.SortOrder)
            {
                case "name":
                    tableQuery = tableQuery.OrderBy(x => x.VehicleMakeEntity.Name);
                    break;
                case "model":
                    tableQuery = tableQuery.OrderBy(x => x.Name);
                    break;
                case "abrv":
                    tableQuery = tableQuery.OrderBy(x => x.Abrv);
                    break;
                default:
                    tableQuery = tableQuery.OrderBy(x => x.Name);
                    break;
            }


            int count = await tableQuery.CountAsync();

            int totalPages = (int)Math.Ceiling(count / (double)5);

            if (totalPages > 0)
                totalPages--;

            tableQuery = tableQuery.Skip(pageSettings.PageSize * pageSettings.PageIndex).Take(pageSettings.PageSize).Include(x => x.VehicleMakeEntity);


            List<VehicleModel> vehicles = Mapper.Map<List<VehicleModel>>(await tableQuery.ToListAsync());

            var model = new Page<IVehicleModel>()
            {
                Items = vehicles.Cast<IVehicleModel>().ToList(),
                PageIndex = pageSettings.PageIndex,
                TotalPages = totalPages,
                SearchString = pageSettings.SearchString,
                SortOrder = pageSettings.SortOrder
            };

            return model;
        }

        public async Task<IVehicleModel> GetVehicleModelByIdAsync(Guid id)
        {
            var vehicle = await UnitOfWork.VehicleModelRepository.TableAsNoTracking.Where(x => x.Id == id).Include(y => y.VehicleMakeEntity).FirstOrDefaultAsync();

            return Mapper.Map<VehicleModel>(vehicle);
        }

        /// <summary>
        /// Creates a vehicle make
        /// </summary>
        /// <param name="vehicleModel"></param>
        /// <returns>true if the creation was successful</returns>
        public async Task<bool> CreateVehicleModelAsync(ICreateVehicleModel vehicleModel)
        {
            var model = Mapper.Map<VehicleModelEntity>(vehicleModel);

            await UnitOfWork.AddAsync(model);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;
        }


        public async Task<bool> UpdateVehicleModelAsync(IUpdateVehicleModel vehicleModel)
        {
            var model = Mapper.Map<VehicleModelEntity>(vehicleModel);

            await UnitOfWork.UpdateAsync(model);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

        public async Task<bool> DeleteVehicleModelAsync(Guid vehicleId)
        {
            await UnitOfWork.DeleteAsync<VehicleModelEntity>(vehicleId);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

    }

}

