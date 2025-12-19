using Identity.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
public sealed class IdentityDatabaseContext(
    DbContextOptions<IdentityDatabaseContext> options) 
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim,
        ApplicationUserToken
    >(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("Identity");
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDatabaseContext).Assembly);
    }
}