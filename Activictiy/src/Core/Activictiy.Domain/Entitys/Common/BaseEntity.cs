namespace Activictiy.Domain.Entitys.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = new Guid();
    public DateTime CreateDate { get; set; } =  DateTime.Now;
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    public virtual bool IsDeleted { get; set; } 
}

