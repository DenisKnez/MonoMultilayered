using AutoMapper;
using Project.Common.System;
using Project.Model;
using Project.WebAPI.AutoMapper.System;
using Project.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyModel, CompanyRestModel>()
                .ForMember(dest => dest.CompanyType, opt => opt.MapFrom(source => source.CompanyTypeModel));

            CreateMap<CompanyRestModel, CompanyModel>()
                .ForMember(source => source.DateCreated, dest => dest.Ignore())
                .ForMember(source => source.DateUpdated, dest => dest.Ignore())
                .ForMember(source => source.IsActive, dest => dest.Ignore());


            CreateMap<PagedList<CompanyModel>, PagedList<CompanyRestModel>>().ConvertUsing<PagedListConverter<CompanyModel, CompanyRestModel>>();

        }

    }
}
