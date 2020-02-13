using AutoMapper;
using Project.Common;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.AutoMapperProfiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            // get
            CreateMap<IVehicleMake, VehicleMakeModel_Model>();

            // page
            CreateMap<List<IVehicleMake>, List<VehicleMakePage_Model>>();
        }

    }
}
