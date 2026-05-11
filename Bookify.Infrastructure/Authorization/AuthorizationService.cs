using Bookify.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _dbContext;
    public AuthorizationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserRolesResponse> GetUserRolesAsync(string identityId)
    {
        var roles = await _dbContext.Set<User>()
                            .Where(u => u.IdentityId == identityId)
                            .Select(u => new UserRolesResponse
                            {
                                Id = u.Id,
                                Roles = u.Roles.ToList()
                            })
                                .FirstAsync();

        return roles;
    }

    public async Task<HashSet<string>> GetUserPermissionsAsync(string identityId)
    {
        var permissions = await _dbContext.Set<User>()
                                    .Where(u => u.IdentityId == identityId)
                                    .SelectMany(u => u.Roles.Select(r => r.Permissions))
                                    .FirstAsync();

        return permissions.Select(p => p.Name).ToHashSet();
    }
}
