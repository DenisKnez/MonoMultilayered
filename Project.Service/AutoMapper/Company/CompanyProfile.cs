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
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyModel, CompanyModel>()
                .ForMember(opt => opt.DateCreated, dest => dest.Ignore())
                .ForMember(opt => opt.DateUpdated, dest => dest.Ignore())
                .ForMember(opt => opt.IsActive, dest => dest.Ignore());
        }
    }
}
