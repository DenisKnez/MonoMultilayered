using AutoMapper;
using Project.Common;
using Project.DAL.DomainModels;
using Project.DAL.IDomainModels;
using Project.Model;
using Project.Model.VehicleMakeDomainModels;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.WebAPI.Models.VehicleMakeRestModels.CRUD;
using Project.Model.Common.IVehicleMakeDomainModels;
using AutoMapper.Configuration;
using Project.WebAPI.Models.VehicleMakeRestModels;

namespace Project.WebAPI.AutoMapperProfiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {


            CreateMap<VehicleMakeEntity, VehicleMake>();
            // get
            CreateMap<VehicleMake, CreateVehicleMakeRestModel>();
            CreateMap<VehicleMake, UpdateVehicleMakeRestModel>();

            CreateMap<VehicleMake, VehicleMakeRestModel>();

            // page
            CreateMap<VehicleMakePageRestModel, Page<IVehicleMake>>();
            CreateMap<Page<IVehicleMake>, VehicleMakePageRestModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items.ConvertAll(m => new VehicleMakeRestModel() { Id = m.Id, Name = m.Name, Abrv = m.Abrv })));
            //.ForMember(x => x.TotalPages, y => y.MapFrom(z => z.TotalPages))

        }

    }
}
