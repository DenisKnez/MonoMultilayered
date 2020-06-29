using AutoMapper;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using Project.WebAPI.AutoMapper.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>()
                .ForMember(dest => dest.CompanyTypeModel, opt => opt.MapFrom(source => source.CompanyType));
            CreateMap<CompanyModel, Company>();

            CreateMap<PagedList<Company>, PagedList<CompanyModel>>().ConvertUsing<PagedListConverter<Company, CompanyModel>>();


        }

    }
}
