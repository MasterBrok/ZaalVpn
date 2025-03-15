using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;
/// <summary>
///
/// </summary>
public class UserClaimApplication : IdentityUserClaim<string>
{
    public virtual UserApplication User { get; private set; }
}