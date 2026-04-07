using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public static class UserErrors
{
    public static Error NotFound = new("User.Found", "The User with the specified identifier was not found");
}