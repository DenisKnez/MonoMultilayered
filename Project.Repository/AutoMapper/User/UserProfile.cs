using AutoMapper;
using Project.Common.System;
using Project.DAL.EntityModels;
using Project.Model;
using Project.WebAPI.AutoMapper.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<PagedList<User>, PagedList<UserModel>>().ConvertUsing<PagedListConverter<User, UserModel>>();



        }
    }
}
