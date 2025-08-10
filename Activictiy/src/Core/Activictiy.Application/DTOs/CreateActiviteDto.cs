namespace Activictiy.Application.DTOs;

public class CreateActiviteDto
{
    public string ActivityType { get; set; }  // "StepCount", "Sleep", "WaterIntake"
    public DateTime ActivityDate { get; set; }
    public int Value { get; set; }
}
