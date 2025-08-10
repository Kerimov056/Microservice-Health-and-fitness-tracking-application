using Activictiy.Persistance.Context;

namespace Activictiy.Persistance.Implementations.Repositories.EntityRepository;

public class WriteActivityRepository : WriteRepository<Domain.Entitys.Activity>, Application.Abstraction.Repositories.IEntityRepsitory.IWriteActivityRepository
{
    public WriteActivityRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
