using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;

/// <summary>
/// 
/// </summary>
public class RoleClaimApplication : IdentityRoleClaim<string>
{
    public virtual RoleApplication Role { get; private set; }
}