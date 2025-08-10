using AutoMapper;
using Activictiy.Application.DTOs.Blog;
using Activictiy.Domain.Entitys;

namespace Activictiy.Persistance.MapperProfiles;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<Blog, BlogCreateDTO>().ReverseMap();
        CreateMap<Blog, BlogGetDTO>().ReverseMap();
        CreateMap<Blog, BlogUpdateDTO>().ReverseMap();
    }
}
