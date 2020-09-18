using AutoMapper;
using Project.Common.System;
using System.Linq;

// This namespace needs to stay the same so the automapper can access this file
namespace Project.WebAPI.AutoMapper.System
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedListConverter(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var items = source.Select(x => Mapper.Map<TSource, TDestination>(x)).ToList();

            return new PagedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}