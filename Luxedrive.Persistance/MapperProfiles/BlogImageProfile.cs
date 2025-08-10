using AutoMapper;
using Activictiy.Application.DTOs.Blog.BlogImage;
using Activictiy.Domain.Entitys;

namespace Activictiy.Persistance.MapperProfiles;

public class BlogImageProfile:Profile
{
    public BlogImageProfile()
    {
        CreateMap<Blogİmages, CreateBlogImageDTO>().ReverseMap();
        CreateMap<Blogİmages, GetBlogImageDTO>().ReverseMap();
        CreateMap<Blogİmages, UpdateBlogImageDTO>().ReverseMap();
    }
}
