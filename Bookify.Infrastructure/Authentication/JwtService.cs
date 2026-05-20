using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Bookify.Infrastructure.Authentication
{
    public sealed class JwtService : IJwtService
    {
        private static readonly Error AuthenticationFailed = new(
                 "Keycloak.AuthenticationFailed",
                 "Failed to acquire access token do to authentication failure");

        private readonly HttpClient _httpClient;
        private readonly KeycloakOptions _keycloakOptions;

        public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> keycloakOptions)
        {
            _httpClient = httpClient;
            _keycloakOptions = keycloakOptions.Value;
        }

        public async Task<Result<string>> GetAccessTokenAsync(
            string Username,
            string password,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", _keycloakOptions.AuthClientId),
                    new("client_secret", _keycloakOptions.AuthClientSecret),
                    new("scope", "openid email"),
                    new("grant_type", "password"),
                    new("username", Username),
                    new("password", password)
                };

                var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

                var response = await _httpClient.PostAsync(
                    "",
                    authorizationRequestContent,
                    cancellationToken);

                var content = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var authorizationToken = await response.Content.ReadFromJsonAsync<AuthorizationToken>();

                if (authorizationToken is null)
                {
                    return Result.Failure<string>(AuthenticationFailed);
                }

                return authorizationToken.AccessToken;
            }
            catch (HttpRequestException)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }
        }

        public async Task<Result<string>> GetAdminAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var authRequestParameters = new KeyValuePair<string, string>[]
                {
                    new("client_id", _keycloakOptions.AdminClientId),
                    new("client_secret", _keycloakOptions.AdminClientSecret),
                    new("grant_type", "client_credentials")
                };

                var authorizationRequestContent =
                    new FormUrlEncodedContent(authRequestParameters);

                var response = await _httpClient.PostAsync(
                    "",
                    authorizationRequestContent,
                    cancellationToken);

                var content = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                var authorizationToken =
                    JsonSerializer.Deserialize<AuthorizationToken>(content);

                if (authorizationToken is null)
                {
                    return Result.Failure<string>(AuthenticationFailed);
                }

                return authorizationToken.AccessToken;
            }
            catch (Exception)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }
        }
    }
}
