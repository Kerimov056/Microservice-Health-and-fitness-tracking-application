using Activictiy.Application.DTOs;
using Activictiy.Domain.Entitys;

namespace Activictiy.Application.Abstraction.Services;

public interface IActivityService
{
    Task AddActivityAsync(CreateActiviteDto activiteDto);
    Task<Activity> GetActivitiesByUserAsync(Guid Id);
    Task<IEnumerable<Activity>> GetActivitiesByUserIdAsync(string userId);
}
