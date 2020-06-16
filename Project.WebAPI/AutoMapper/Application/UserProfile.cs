using AutoMapper;
using Project.Model;
using Project.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserRestModel>();

            CreateMap<UserRestModel, UserModel>()
                .ForMember(source => source.DateCreated, dest => dest.Ignore())
                .ForMember(source => source.DateUpdated, dest => dest.Ignore())
                .ForMember(source => source.IsActive, dest => dest.Ignore());


        }

    }
}
