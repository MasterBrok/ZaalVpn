using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;
/// <summary>
/// 
/// </summary>
public class UserRoleApplication : IdentityUserRole<string>
{
    public virtual UserApplication User { get; private set; }
    public virtual RoleApplication Role { get; private set; }
}