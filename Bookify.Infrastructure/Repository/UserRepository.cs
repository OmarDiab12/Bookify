using Bookify.Domain.User;

namespace Bookify.Infrastructure.Repository;

internal sealed class UserRepository(ApplicationDbContext dbContext) :  Repository<User>(dbContext), IUserRepository
{
    public override void Add(User user)
    {
        foreach (var role in user.Roles)
        {
            dbContext.Attach(role);
        }

        dbContext.Add(user);
    }
}