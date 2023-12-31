using Microsoft.AspNetCore.Identity;

namespace Finance.Domain.Identity;

public class Role : IdentityRole<int>
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}
