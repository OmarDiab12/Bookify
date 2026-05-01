using Bookify.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbcontext.Database.Migrate();
    }
}