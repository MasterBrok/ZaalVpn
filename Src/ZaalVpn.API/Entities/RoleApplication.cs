using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;

/// <summary>
/// 
/// </summary>
public class RoleApplication : IdentityRole
{
    public virtual ICollection<UserRoleApplication> UserRoles { get; private set; }
    public virtual ICollection<RoleClaimApplication> RoleClaims { get; private set; }
}