using Activictiy.Application.DTOs.Blog.BlogImage;

namespace Activictiy.Application.DTOs.Blog;

public class BlogCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CreateBlogImageDTO> BlogImages { get; set; }
}
