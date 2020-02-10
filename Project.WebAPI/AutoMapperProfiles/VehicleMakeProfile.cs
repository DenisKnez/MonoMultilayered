using AutoMapper;
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
            // create
            CreateMap<VehicleMake, VehicleMakeModel_Model>();



            //CreateMap<CreateMakeModel, VehicleMake>();

            //// edit
            //CreateMap<EditMakeModel, VehicleMake>();
            //CreateMap<VehicleMake, EditMakeModel>();

            //// page
            //CreateMap<VehicleMakeModel, VehicleMake>();
        }

    }
}
