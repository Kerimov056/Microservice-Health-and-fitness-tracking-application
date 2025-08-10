using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Domain.Entitys;

public class Activity : BaseEntity
{
    public string ActivityType { get; set; }  // "StepCount", "Sleep", "WaterIntake"
    public DateTime ActivityDate { get; set; }
    public int Value { get; set; }

    // İstifadəçi ID-si
    public string UserId { get; set; } 

}
