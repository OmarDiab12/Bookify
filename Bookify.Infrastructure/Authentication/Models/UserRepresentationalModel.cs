using Bookify.Domain.User;

namespace Bookify.Infrastructure.Authentication.Models
{
    public sealed class UserRepresentationalModel
    {
        public Dictionary<string, string> Access { get; set; }
        public Dictionary<string, List<string>> Attributes { get; set; }
        public Dictionary<string, string> ClientRoles { get; set; }
        public long? CreatedTimeStamp { get; set; }
        public CredentialRepresentationalModel[] Credentials { get; set; }
        public string[] DisabledCredentialTypes { get; set; }
        public string Email { get; set; }
        public bool? EmailVerified { get; set; }
        public bool Enabled { get; set; }
        public string FederationalLink { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string[] RequiredActions { get; private set; }
        public string username { get; set; }

        internal static UserRepresentationalModel FromUser(User user)
        {
            return new UserRepresentationalModel
            {
                FirstName = user.FirstName.value,
                LastName = user.LastName.value,
                Email = user.Email.value,
                username = user.Email.value,
                Enabled = true,
                EmailVerified = true,
                CreatedTimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Attributes = new Dictionary<string, List<string>>(),
                RequiredActions = Array.Empty<string>()

            };
        }
    }
}
