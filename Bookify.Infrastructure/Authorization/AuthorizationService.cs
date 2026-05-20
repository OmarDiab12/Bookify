using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.User;
using Bookify.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ICashService _cache;
    public AuthorizationService(ApplicationDbContext dbContext, ICashService cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<UserRolesResponse> GetUserRolesAsync(string identityId)
    {
        var chcheKey = $"auth:roles-{identityId}";

        var cachedRoles = await _cache.GetAsync<UserRolesResponse>(chcheKey);
        if (cachedRoles is not null)
        {
            return cachedRoles;
        }

        var roles = await _dbContext.Set<User>()
                            .Where(u => u.IdentityId == identityId)
                            .Select(u => new UserRolesResponse
                            {
                                Id = u.Id,
                                Roles = u.Roles.ToList()
                            })
                                .FirstOrDefaultAsync();

        await _cache.SetAsync(chcheKey, roles);
        
        return roles;
    }

    public async Task<HashSet<string>> GetUserPermissionsAsync(string identityId)
    {
        var chcheKey = $"auth:permissions-{identityId}";

        var cachedpermissions = await _cache.GetAsync<HashSet<string>>(chcheKey);
        if (cachedpermissions is not null)
        {
            return cachedpermissions;
        }
        var permissions = await _dbContext.Set<User>()
                                    .Where(u => u.IdentityId == identityId)
                                    .SelectMany(u => u.Roles.Select(r => r.Permissions))
                                    .FirstOrDefaultAsync();

        var permissionsSet = permissions?.Select(p => p.Name).ToHashSet() ?? new HashSet<string>();
        await _cache.SetAsync(chcheKey, permissionsSet);
        return permissionsSet;
    }
}
