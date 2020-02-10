using Project.DAL.Context;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Repository.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Repository.Repositories
{
    public class VehicleMakeRepository : Repository<VehicleMake>, IVehicleMakeRepository
    {

        public VehicleMakeRepository(VehicleContext context) : base(context)
        {

        }

        


    }
}
