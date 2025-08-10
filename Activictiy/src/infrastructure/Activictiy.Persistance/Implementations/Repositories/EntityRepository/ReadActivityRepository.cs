using Activictiy.Domain.Entitys;
using Activictiy.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Activictiy.Persistance.Implementations.Repositories.EntityRepository;

public class ReadActivityRepository : ReadRepository<Domain.Entitys.Activity>, Application.Abstraction.Repositories.IEntityRepsitory.IReadActivityRepository
{
    private readonly AppDbContext _context;
    public ReadActivityRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesByUserIdAsync(string userId)
    {
        return await _context.Activities
                             .Where(a => a.UserId == userId)
                             .ToListAsync();
    }
}
