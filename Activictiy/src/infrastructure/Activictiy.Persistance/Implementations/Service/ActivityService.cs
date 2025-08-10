using Activictiy.Application.Abstraction.Repositories.IEntityRepsitory;
using Activictiy.Application.Abstraction.Services;
using Activictiy.Application.DTOs;
using Activictiy.Domain.Entitys;
using Activictiy.Persistance.Exceptions;

namespace Activictiy.Persistance.Implementations.Service;

public class ActivityService : IActivityService
{
    private readonly IReadActivityRepository _readActivityRepository;
    private readonly IWriteActivityRepository _writeActivityRepository;
    public ActivityService(IReadActivityRepository readActivityRepository, IWriteActivityRepository writeActivityRepository)
    {
        _readActivityRepository = readActivityRepository;
        _writeActivityRepository = writeActivityRepository;
    }
    public async Task AddActivityAsync(CreateActiviteDto createActiviteDto)
    {
        Activity activity = new Activity()
        {
            ActivityType = createActiviteDto.ActivityType,
            ActivityDate = DateTime.SpecifyKind(createActiviteDto.ActivityDate, DateTimeKind.Utc),
            Value = createActiviteDto.Value
        };

        await _writeActivityRepository.AddAsync(activity);
        await _writeActivityRepository.SaveChangeAsync();
    }


    public async Task<Activity> GetActivitiesByUserAsync(Guid Id)
    {
        return await _readActivityRepository.GetByIdAsync(Id);
    }

    public async Task<IEnumerable<Activity>> GetActivitiesByUserIdAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
             throw new NotFoundException("UserId query parameter is required.");

        var activities = await _readActivityRepository.GetActivitiesByUserIdAsync(userId);
        return activities ?? throw new NotFoundException("Activities not found for the provided userId.");
    }
}
