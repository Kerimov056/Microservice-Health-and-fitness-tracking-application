using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Domain.Entitys;

public class CarCatogory:BaseEntity
{
    public string Category { get; set; }
    public List<Car> cars { get; set; }
}
