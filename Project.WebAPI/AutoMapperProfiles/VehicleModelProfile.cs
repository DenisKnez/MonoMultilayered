using Project.DAL.IDomainModels;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.WebAPI.Models.VehicleModelRestModels;
using Project.Model.VehicleModelDomainModels;
using Project.DAL.DomainModels;
using Project.WebAPI.Models.VehicleModelRestModels.CRUD;
using Project.Model.VehicleMakeDomainModels;
using Project.Model.Common.IVehicleModelDomainModels;

namespace Project.WebAPI.AutoMapperProfiles
{
    public class VehicleModelProfile : Profile
    {
        public VehicleModelProfile()
        {
            CreateMap<VehicleModelEntity, VehicleModel>()
                //.ForMember(x => x.VehicleMakeId, y => y.MapFrom(z => z.VehicleMakeEntityId))
                .ForMember(x => x.VehicleMake, y => y.MapFrom(z => new VehicleMake() { Id = z.Id, Name = z.Name, Abrv = z.Abrv}));

            // crud
            CreateMap<VehicleModel, CreateVehicleModelRestModel>();
            CreateMap<VehicleModel, UpdateVehicleModelRestModel>();

            // page
            CreateMap<Page<IVehicleModel>, VehicleModelPageRestModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items.ConvertAll(m => new VehicleModelRestModel() { Id = m.Id, Name = m.Name, Abrv = m.Abrv })));

        }


    }
}
