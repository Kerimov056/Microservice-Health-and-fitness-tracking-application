using Activictiy.Application.Abstraction.Repositories.IEntityRepsitory;
using Activictiy.Application.Abstraction.Services;
using Contracts.Events;
using MassTransit;

namespace Activictiy.Persistance.Implementations.Service;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IWriteActivityRepository _writeActivityRepository;

    public UserCreatedConsumer(IWriteActivityRepository writeActivityRepository)
    {
        _writeActivityRepository = writeActivityRepository;
    }
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;

        var activity = new Activictiy.Domain.Entitys.Activity
        {
            ActivityType = "Welcome",
            ActivityDate = DateTime.UtcNow,
            Value = 0,
            UserId = message.UserId.ToString(),
        };

        await _writeActivityRepository.AddAsync(activity);
        await _writeActivityRepository.SaveChangeAsync();
        await Task.CompletedTask;
    }
}
