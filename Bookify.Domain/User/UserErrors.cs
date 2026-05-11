using Bookify.Domain.Abstractions;

namespace Bookify.Domain.User
{
    public static class UserErrors
    {
        public static Error NotFound = new("User.NotFound", "The user with the specified identifier was not found");
        public static Error InvalidCredentials = new("User.InvalidCredentials", "The provided credentials are invalid");
    }
}

