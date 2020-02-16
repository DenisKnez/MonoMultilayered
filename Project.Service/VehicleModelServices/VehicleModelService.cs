using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Common.Interfaces;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model;
using Project.Model.Common;
using Project.Model.Common.IVehicleMakeDomainModels;
using Project.Model.Common.IVehicleModelDomainModels;
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
                tableQuery = tableQuery.Where(x => x.Name.Contains(pageSettings.SearchString));
            }


            switch (pageSettings.SortOrder)
            {
                case "name":
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


            //return new Page<IVehicleMake>() { Items = await tableQuery.ToListAsync(), TotalPages = totalPages, PageIndex = pageSettings.PageIndex };


            var something = await tableQuery.ToListAsync();

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

        public async Task<IVehicleModelEntity> GetVehicleModelByIdAsync(Guid id)
        {

            return await UnitOfWork.VehicleModelRepository.GetByIdAsync(id);

        }

        /// <summary>
        /// Creates a vehicle make
        /// </summary>
        /// <param name="vehicleMake"></param>
        /// <returns>true if the creation was successful</returns>
        public async Task<bool> CreateVehicleModelAsync(IVehicleModel vehicleMake)
        {
            //var vehicle = await VehicleMakeRepository.AddAsync();

            var model = Mapper.Map<VehicleMakeEntity>(vehicleMake);

            await UnitOfWork.AddAsync(model);

            var result = await UnitOfWork.CommitAsync();


            return result == 0 ? false : true;
        }


        public async Task<bool> UpdateVehicleModelAsync(IVehicleModel vehicleMake)
        {
            var model = Mapper.Map<VehicleMakeEntity>(vehicleMake);

            await UnitOfWork.UpdateAsync(model);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

        public async Task<bool> DeleteVehicleModelAsync(Guid vehicleId)
        {
            await UnitOfWork.DeleteAsync<VehicleMakeEntity>(vehicleId);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

    }

}

