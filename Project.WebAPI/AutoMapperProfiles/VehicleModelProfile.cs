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
            //CreateMap<VehicleModelEntity, VehicleModel>()
            //    .ConvertUsing(new VehicleModelTypeConverter());


            CreateMap<VehicleModelEntity, VehicleModel>()
                .ForMember(x => x.VehicleMake, y => y.MapFrom(z => new VehicleMake() { Id = z.VehicleMakeEntity.Id, Name = z.VehicleMakeEntity.Name, Abrv = z.VehicleMakeEntity.Abrv}));

            // crud
            CreateMap<VehicleModel, CreateVehicleModelRestModel>();
            CreateMap<VehicleModel, UpdateVehicleModelRestModel>();

            //CreateMap<VehicleModelEntity, VehicleModel>();

            // page
            CreateMap<Page<IVehicleModel>, VehicleModelPageRestModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items.ConvertAll(m => new VehicleModelRestModel() { Id = m.Id, Name = m.Name, Abrv = m.Abrv, VehicleMake = m.VehicleMake})));

        }

        public class VehicleModelTypeConverter : ITypeConverter<VehicleModelEntity, VehicleModel>
        {
            public VehicleModel Convert(VehicleModelEntity source, VehicleModel destination, ResolutionContext context)
            {
                return new VehicleModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Abrv = source.Abrv,
                    VehicleMake = new VehicleMake()
                    {
                        Id = source.VehicleMakeEntity.Id,
                        Name = source.VehicleMakeEntity.Name,
                        Abrv = source.VehicleMakeEntity.Abrv
                    }
                };


            }
        }


    }
}
