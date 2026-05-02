using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.Login
{
    public sealed record LoginUserCommand(string Email, string Password) : ICommand<AccessTokenResponse>;
}
