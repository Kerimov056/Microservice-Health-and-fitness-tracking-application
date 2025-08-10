using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Domain.Entitys;

public class Blogİmages:BaseEntity
{
    public string ImagePath { get; set; }
    public Guid blogId { get; set; }
    public Blog blog { get; set; }
}
