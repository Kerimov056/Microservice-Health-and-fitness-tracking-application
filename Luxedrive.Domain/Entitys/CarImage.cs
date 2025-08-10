using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Domain.Entitys;

public class CarImage:BaseEntity
{
    public string ImagePath { get; set; }
    public Guid carId { get; set; }
    public Car car { get; set; }
}
