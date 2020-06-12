using Microsoft.EntityFrameworkCore;
using Project.DAL;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Repository.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Repositories
{
    public class VehicleMakeRepository : Repository<VehicleMakeEntity>, IVehicleMakeRepository
    {

        public VehicleMakeRepository(VehicleContext context) : base(context)
        {

        }


        public async Task<List<VehicleMakeEntity>> GetVehicles()
        {
            return await Entities.ToListAsync();
        }
        


    }
}
