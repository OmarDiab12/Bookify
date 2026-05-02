namespace Bookify.Infrastructure.Authentication.Models
{
    public class CredentialRepresentationalModel
    {
        public string Value { get; internal set; }
        public string Type { get; internal set; }
        public bool Temporary { get; internal set; }
    }
}