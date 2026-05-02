namespace Bookify.Api.Controllers.Users
{
    public class LoginUserRequest
    {
        public string Email { get; internal set; }
        public string Password { get; internal set; }
    }
}