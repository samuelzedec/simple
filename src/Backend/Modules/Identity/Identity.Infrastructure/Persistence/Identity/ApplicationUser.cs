using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Persistence.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public Guid UserDomainId { get; set; }
    public User User { get; set; } = null!;
    public bool IsDeleted { get; set; }
}