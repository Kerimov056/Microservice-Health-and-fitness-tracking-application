using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Domain.Entitys;

public class Blog : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Blogİmages> blogImages { get; set; }
}
