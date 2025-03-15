using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;
/// <summary>
/// 
/// </summary>
public class UserLoginApplication : IdentityUserLogin<string>
{
    public virtual UserApplication User { get; private set; }
}