using Activictiy.Application.DTOs.Blog.BlogImage;

namespace Activictiy.Application.DTOs.Blog;

public class BlogGetDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<GetBlogImageDTO> Images { get; set; }
}
