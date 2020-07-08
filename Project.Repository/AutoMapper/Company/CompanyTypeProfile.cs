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
    public class CompanyTypeProfile : Profile
    {
        public CompanyTypeProfile()
        {
            CreateMap<CompanyType, CompanyTypeModel>();
            CreateMap<CompanyTypeModel, CompanyType>();

            CreateMap<PagedList<CompanyType>, PagedList<CompanyTypeModel>>().ConvertUsing<PagedListConverter<CompanyType, CompanyTypeModel>>();
        }
    }
}
