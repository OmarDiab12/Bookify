using Bookify.Domain.Abstractions;
using Bookify.Domain.User.Events;

namespace Bookify.Domain.User;
public class User : Entity
{
    private readonly List<Role> _roles = [];
    private readonly List<Permission> _permissions = [];
    private User(Guid id, FirstName firstName, LastName lastName, Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    private User()
    {

    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;
    public IReadOnlyCollection<Role> Roles => [.. _roles];
    public IReadOnlyCollection<Permission> Permissions => [.. _permissions];

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        User? user = new(Guid.NewGuid(),firstName, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        
        user._roles.Add(Role.Registered);
        user._permissions.Add(Permission.UsersRead);

        return user;
    }

        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
    }
}