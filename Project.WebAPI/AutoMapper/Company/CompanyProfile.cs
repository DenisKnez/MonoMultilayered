using AutoMapper;
using Project.Common.System;
using Project.Model;
using Project.WebAPI.AutoMapper.System;

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

            CreateMap<LeastEmployeesCompanyModel, CompanyLeastAmountEmployeesRestModel>()
                .ForMember(source => source.Name, dest => dest.MapFrom(x => x.Name))
                .ForMember(source => source.Id, dest => dest.MapFrom(x => x.Id));
        }
    }
}