using Activictiy.Application.Abstraction.Services;
using Activictiy.Application.DTOs;
using Activictiy.Domain.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace Activictiy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivitiesController(IActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpPost("/post")]
    public async Task<IActionResult> AddActivity(CreateActiviteDto activiteDto)
    {

        await _activityService.AddActivityAsync(activiteDto);
        return Ok();
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetActivitiesByUser(Guid Id)
    {
        var activities = await _activityService.GetActivitiesByUserAsync(Id);
        return Ok(activities);
    }

    [HttpGet("/activicatiy")]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivitiesByUserId([FromQuery] string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest("UserId query parameter is required.");

        var activities = await _activityService.GetActivitiesByUserIdAsync(userId);

        if (activities == null)
            return NotFound();

        return Ok(activities);
    }
}
