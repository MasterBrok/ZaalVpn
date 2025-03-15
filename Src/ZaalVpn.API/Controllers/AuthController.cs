using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API.Entities;
using ZaalVpn.ViewModel;

namespace ZaalVpn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<UserApplication> _signInManager;

        private ResultModel result = new();
        private readonly UserManager<UserApplication> _userManager;


        public AuthController(SignInManager<UserApplication> signInManager, UserManager<UserApplication> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("Login")]
        public async Task<ResultModel> Login(LoginViewModel login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName.Contains(login.UserName));
            if (user is null)
                return result.NotFound();


            var res = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);
            if (!res.Succeeded)
                return result.FailedLogin();


            return result.Succeeded();
        }

        [HttpPost]
        public async Task<ResultModel> Logout()
        {
            if (!_signInManager.IsSignedIn(base.User))
                return result.Failed("کاربری لاگین نشده است");
            await _signInManager.SignOutAsync();
            return result.Succeeded();
        }
    }
}
