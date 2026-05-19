using Bookify.Domain.User;
using System.Net.Http.Json;
using Bookify.Infrastructure.Authentication.Models;
namespace Bookify.Application.Abstractions.Authentication
{
    internal class AuthenticationService : IAuthenticationService
    {
        private const string PasswordCredentialType = "password";
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            var userRepresentationalModel =  UserRepresentationalModel.FromUser(user);
            userRepresentationalModel.Credentials =
            [
                new ()
                {
                    Value = password,
                    Temporary = false,
                    Type = PasswordCredentialType

                }
            ];

            var response = await _httpClient.PostAsJsonAsync(
                "users",
                userRepresentationalModel,
                cancellationToken);

            
            return ExtractIdentityIdFromLocationHeader(response);
        }

        private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage response)
        {
            const string usersSegmentName = "users/";
            var locationHeader = (response.Headers.Location?.PathAndQuery) ?? throw new InvalidOperationException("Location header is missing.");

            var userSegmentValueIndex = locationHeader.IndexOf(usersSegmentName, StringComparison.InvariantCultureIgnoreCase);
           
            var userIdentityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

            return userIdentityId;
        }
    }
}
