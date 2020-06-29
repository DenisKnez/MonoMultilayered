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
    public class CompanyTypeProfile : Profile
    {
        public CompanyTypeProfile()
        {
            CreateMap<CompanyTypeModel, CompanyTypeRestModel>();

            CreateMap<CompanyTypeRestModel, CompanyTypeModel>()
                .ForMember(source => source.DateCreated, dest => dest.Ignore())
                .ForMember(source => source.DateUpdated, dest => dest.Ignore())
                .ForMember(source => source.IsActive, dest => dest.Ignore());


            CreateMap<PagedList<CompanyTypeModel>, PagedList<CompanyTypeRestModel>>().ConvertUsing<PagedListConverter<CompanyTypeModel, CompanyTypeRestModel>>();

        }

    }
}
