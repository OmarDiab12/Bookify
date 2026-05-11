using Bookify.Domain.User;

namespace Bookify.Infrastructure.Authorization;

public class UserRolesResponse 
{
    public Guid Id { get; set; }
    public List<Role> Roles { get; set; }
}
