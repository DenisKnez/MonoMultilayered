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
using Project.Model.VehicleModelDomainModels.CRUD;

namespace Project.WebAPI.AutoMapperProfiles
{
    public class VehicleModelProfile : Profile
    {

        public VehicleModelProfile()
        {

            //create
            CreateMap<CreateVehicleModelRestModel, CreateVehicleModel>();
            CreateMap<CreateVehicleModel, VehicleModelEntity>()
                .ForMember(x => x.VehicleMakeEntityId, y => y.MapFrom(z => z.VehicleMakeId));

            // update
            CreateMap<UpdateVehicleModelRestModel, UpdateVehicleModel>();
            CreateMap<UpdateVehicleModel, VehicleModelEntity>()
                .ForMember(x => x.VehicleMakeEntityId, y => y.MapFrom(z => z.VehicleMakeId));



            // get
            CreateMap<VehicleModelEntity, VehicleModel>()
                .ForMember(x => x.VehicleMake, y => y.MapFrom(z => new VehicleMake() { Id = z.VehicleMakeEntity.Id, Name = z.VehicleMakeEntity.Name, Abrv = z.VehicleMakeEntity.Abrv}));
            CreateMap<VehicleModel, VehicleModelRestModel>();


            //CreateMap<VehicleModelEntity, VehicleModel>()
            //    .ForMember(x => x.VehicleMake, y =>
            //            y.MapFrom(z => new VehicleMakeEntity()
            //            {
            //                Id = z.VehicleMakeEntity.Id,
            //                Name = z.VehicleMakeEntity.Name,
            //                Abrv = z.VehicleMakeEntity.Abrv
            //            }));


            // page
            CreateMap<Page<IVehicleModel>, VehicleModelPageRestModel>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Items.ConvertAll(m => new VehicleModelRestModel() { Id = m.Id, Name = m.Name, Abrv = m.Abrv, VehicleMake = m.VehicleMake})));

        }

        //public class VehicleModelTypeConverter : ITypeConverter<VehicleModelEntity, VehicleModel>
        //{
        //    public VehicleModel Convert(VehicleModelEntity source, VehicleModel destination, ResolutionContext context)
        //    {
        //        return new VehicleModel()
        //        {
        //            Id = source.Id,
        //            Name = source.Name,
        //            Abrv = source.Abrv,
        //            VehicleMake = new VehicleMake()
        //            {
        //                Id = source.VehicleMakeEntity.Id,
        //                Name = source.VehicleMakeEntity.Name,
        //                Abrv = source.VehicleMakeEntity.Abrv
        //            }
        //        };


        //    }
        //}


    }
}
