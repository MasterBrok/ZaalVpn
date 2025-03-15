using Microsoft.AspNetCore.Identity;

namespace ZaalVpn.API.Entities;

/// <summary>
/// 
/// </summary>
public class UserApplication : IdentityUser
{
    /// <summary>
    /// 
    /// </summary>


    public virtual ICollection<UserClaimApplication> UserClaims { get; private set; }
    public virtual ICollection<UserLoginApplication> UserLogins { get; private set; }
    public virtual ICollection<UserTokenApplication> UserTokens { get; private set; }
    public virtual ICollection<UserRoleApplication> Role { get; set; }

    public DateTime CreationTimeAt { get; set; }
    public DateTime LastOnlineAt { get; set; }
    public GenderEntity Gender { get; set; }
    public string GenderId { get; set; }
    
    public string ShortId { get; set; }


    public UserApplication()
    {
        CreationTimeAt = DateTime.Now;
    }

}