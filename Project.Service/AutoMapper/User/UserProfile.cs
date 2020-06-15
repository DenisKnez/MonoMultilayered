using AutoMapper;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.AutoMapper.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserModel>()
                .ForMember(opt => opt.DateCreated, dest => dest.Ignore())
                .ForMember(opt => opt.DateUpdated, dest => dest.Ignore())
                .ForMember(opt => opt.IsActive, dest => dest.Ignore());
        }

    }
}
