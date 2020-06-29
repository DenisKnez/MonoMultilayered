using AutoMapper;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using Project.WebAPI.AutoMapper.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service
{
    public class CompanyTypeProfile : Profile
    {
        public CompanyTypeProfile()
        {
            CreateMap<CompanyTypeModel, CompanyTypeModel>()
                .ForMember(opt => opt.DateCreated, dest => dest.Ignore())
                .ForMember(opt => opt.DateUpdated, dest => dest.Ignore())
                .ForMember(opt => opt.IsActive, dest => dest.Ignore());
        }
    }
}
