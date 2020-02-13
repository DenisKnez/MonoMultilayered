using Project.Common;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Repository.Common;
using Project.Repository.Common.Repositories;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Project.Common.Interfaces;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public VehicleMakeService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public async Task<IPage<IVehicleMake>> GetPaginatedFilteredListAsync(IPageSettings pageSettings)
        {
            IQueryable<IVehicleMake> tableQuery = UnitOfWork.VehicleMakeRepository.TableAsNoTracking;

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


            return new Page<IVehicleMake>() { Items = await tableQuery.ToListAsync(), TotalPages = totalPages, PageIndex = pageSettings.PageIndex };

        }

        public async Task<IVehicleMake> GetVehicleMakeByIdAsync(Guid id)
        {
            
            return await UnitOfWork.VehicleMakeRepository.GetByIdAsync(id);

        }

        /// <summary>
        /// Creates a vehicle make
        /// </summary>
        /// <param name="vehicleMake"></param>
        /// <returns>true if the creation was successful</returns>
        public async Task<bool> CreateVehicleMakeAsync(IVehicleMake vehicleMake)
        {
            //var vehicle = await VehicleMakeRepository.AddAsync();

            await UnitOfWork.AddAsync(vehicleMake);

            var result = await UnitOfWork.CommitAsync();


            return result == 0 ? false : true;
        }


        public async Task<bool> UpdateVehicleMakeAsync(IVehicleMake vehicleMake)
        {
            await UnitOfWork.UpdateAsync(vehicleMake);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

        public async Task<bool> DeleteVehicleMakeAsync(Guid vehicleId)
        {
            await UnitOfWork.DeleteAsync<VehicleMake>(vehicleId);

            var result = await UnitOfWork.CommitAsync();

            return result == 0 ? false : true;

        }

    }
}
