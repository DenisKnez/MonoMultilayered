using AutoMapper;
using Project.DAL.EntityModels;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Repository.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}
