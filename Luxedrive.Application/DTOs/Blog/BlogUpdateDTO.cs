using Activictiy.Application.DTOs.Blog.BlogImage;

namespace Activictiy.Application.DTOs.Blog;

public class BlogUpdateDTO
{
    public Guid BlogId { get; set; }
    public string? changeTitle { get; set; }
    public string? changeDescription { get; set; }
    public List<UpdateBlogImageDTO>? updateBlogImageDTOs { get; set; }
}
