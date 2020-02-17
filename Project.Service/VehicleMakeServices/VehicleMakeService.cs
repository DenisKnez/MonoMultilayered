using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Project.Common.Interfaces;
using Project.Model;
using Project.Model.Common;
using Project.Model.Common.IVehicleMakeDomainModels;
using AutoMapper;
using Project.Service.Common.IVehicleMakeServices;
using Project.Model.VehicleMakeDomainModels;
using Project.Model.Common.IVehicleMakeDomainModels.CRUD;

namespace Project.Service.VehicleMakeServices
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }

        public VehicleMakeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }


        public async Task<IPage<IVehicleMake>> GetPaginatedFilteredListAsync(IPageSettings pageSettings)
        {
            IQueryable<IVehicleMakeEntity> tableQuery = UnitOfWork.VehicleMakeRepository.TableAsNoTracking;

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

            tableQuery = tableQuery.Skip(pageSettings.PageSize * pageSettings.PageIndex).Take(pageSettings.PageSize);


            //return new Page<IVehicleMake>() { Items = await tableQuery.ToListAsync(), TotalPages = totalPages, PageIndex = pageSettings.PageIndex };


            List<VehicleMake> vehilces = Mapper.Map<List<VehicleMake>>(await tableQuery.ToListAsync());

            var model = new Page<IVehicleMake>()
            {
                Items = vehilces.Cast<IVehicleMake>().ToList(),
                PageIndex = pageSettings.PageIndex,
                TotalPages = totalPages,
                SearchString = pageSettings.SearchString,
                SortOrder = pageSettings.SortOrder
            };

            return model;
        }

        public async Task<List<IVehicleMake>> GetVehiclesAsync()
        {
            var something = await UnitOfWork.VehicleMakeRepository.Table.ToListAsync();

            return Mapper.Map<List<VehicleMake>>(something).Cast<IVehicleMake>().ToList();
        }



        public async Task<IVehicleMake> GetVehicleMakeByIdAsync(Guid id)
        {
            
            return Mapper.Map<VehicleMake>(await UnitOfWork.VehicleMakeRepository.GetByIdAsync(id));

        }

        /// <summary>
        /// Creates a vehicle make
        /// </summary>
        /// <param name="vehicleMake"></param>
        /// <returns>true if the creation was successful</returns>
        public async Task<bool> CreateVehicleMakeAsync(ICreateVehicleMake vehicleMake)
        {
            //var vehicle = await VehicleMakeRepository.AddAsync();

            var model = Mapper.Map<VehicleMakeEntity>(vehicleMake);

            await UnitOfWork.AddAsync(model);

            var result = await UnitOfWork.CommitAsync();


            return result == 0 ? false : true;
        }


        public async Task<bool> UpdateVehicleMakeAsync(IUpdateVehicleMake vehicleMake)
        {
            var model = Mapper.Map<VehicleMakeEntity>(vehicleMake);

            await UnitOfWork.UpdateAsync(model);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

        public async Task<bool> DeleteVehicleMakeAsync(Guid vehicleId)
        {
            await UnitOfWork.DeleteAsync<VehicleMakeEntity>(vehicleId);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

    }
}
