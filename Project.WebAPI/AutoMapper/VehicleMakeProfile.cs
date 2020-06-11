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
using Project.Model.VehicleMakeDomainModels.CRUD;

namespace Project.WebAPI.AutoMapperProfiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            //create
            CreateMap<CreateVehicleMakeRestModel, CreateVehicleMake>();
            CreateMap<CreateVehicleMake, VehicleMakeEntity>();

            // update
            CreateMap<UpdateVehicleMakeRestModel, UpdateVehicleMake>();
            CreateMap<UpdateVehicleMake, VehicleMakeEntity>();


            // get
            CreateMap<VehicleMakeEntity, VehicleMake>();
            CreateMap<VehicleMake, VehicleMakeRestModel>();

            // page
            CreateMap<VehicleMakePageRestModel, Page<IVehicleMake>>();
            CreateMap<Page<IVehicleMake>, VehicleMakePageRestModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items.ConvertAll(m => new VehicleMakeRestModel() { Id = m.Id, Name = m.Name, Abrv = m.Abrv })));

        }

    }
}
