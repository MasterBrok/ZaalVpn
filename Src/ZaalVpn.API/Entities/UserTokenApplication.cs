using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;

/// <summary>
/// 
/// </summary>
public class UserTokenApplication : IdentityUserToken<string>
{
    public virtual UserApplication User { get; private set; }
}