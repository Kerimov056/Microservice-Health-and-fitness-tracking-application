using Activictiy.Domain.Entitys;

namespace Activictiy.Application.Abstraction.Repositories.IEntityRepsitory;

public interface IReadActivityRepository : IReadRepository<Domain.Entitys.Activity>
{
    Task<IEnumerable<Activity>> GetActivitiesByUserIdAsync(string userId);

}
