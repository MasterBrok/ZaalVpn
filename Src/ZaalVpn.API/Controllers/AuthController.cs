using System.Net;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<ResultModel> Login(LoginViewModel login)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName.Contains(login.UserName));
            if (user is null)
                return result.NotFound();

            var res = await _signInManager.PasswordSignInAsync(user, login.Password, true, false);

            if (!res.Succeeded)
                return result.FailedLogin();


            return result.Succeeded(OperationMessage.Done);
        }


        [HttpPost("Registration")]
        [AllowAnonymous]
        public async Task<ResultModel> CreateAccount(CreateAccountViewModel account)
        {
            var user = new UserApplication()
            {
                Email = account.Email,
                UserName = account.UserName,
                GenderId = account.GenderId,
            };
            if (await _userManager.Users.AnyAsync(a => a.UserName.Contains(account.UserName)))
                return result.Failed(OperationMessage.Duplicated);
            var create = await _userManager.CreateAsync(user, account.Password);
            if (!create.Succeeded)
                return result.Set(HttpStatusCode.BadRequest).Failed(create.Errors.First().Description);
            var role = await _userManager.AddToRoleAsync(user, Roles.User);
            if(!role.Succeeded)
                return result.Set(HttpStatusCode.BadRequest).Failed(role.Errors.First().Description);

            return result.Succeeded(OperationMessage.Done);
        }

        [HttpPost("Logout")]
        public async Task<ResultModel> Logout()
        {
            if (!_signInManager.IsSignedIn(base.User))
                return result.Failed("کاربری لاگین نشده است");
            await _signInManager.SignOutAsync();
            return result.Succeeded();
        }
    }
}
