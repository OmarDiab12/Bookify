namespace Bookify.Domain.User;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(User user);
}