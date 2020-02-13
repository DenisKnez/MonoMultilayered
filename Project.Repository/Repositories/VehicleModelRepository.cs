using Project.DAL.Context;
using Project.DAL.DomainModels;
using Project.Repository.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository.Repositories
{
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {
        public VehicleModelRepository(VehicleContext context) : base(context)
        {

        }
    }
}
