using Bookify.Domain.User;

namespace Bookify.Infrastructure.Repository;

internal sealed class UserRepository :  Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}