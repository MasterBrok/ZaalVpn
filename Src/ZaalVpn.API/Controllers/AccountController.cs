using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API.Entities;
using ZaalVpn.ViewModel;

namespace ZaalVpn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policies.Admin)]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> _userManager;
        private ResultModel result = new();
        public AccountController(UserManager<UserApplication> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Edit")]
        public async Task<ResultModel> UpdateAccount([FromBody] UserApplication user)
        {
            var isUpdate = await _userManager.UpdateAsync(user);
            if (!isUpdate.Succeeded)
                return result.Set(HttpStatusCode.BadRequest).Failed(OperationMessage.FailedUpdate, isUpdate.Errors.First().Description);
            return result.Succeeded(OperationMessage.Update);
        }

        [HttpGet("AccountDetail")]
        public async Task<ApiResponse<UserApplication>> GetAccount(string userName = "", string id = "", string email = "")
        {
            var user = new UserApplication();
            if (!string.IsNullOrEmpty(userName))
                user = await _userManager.FindByNameAsync(userName);
            else if (!string.IsNullOrEmpty(id))
                user = await _userManager.FindByIdAsync(id);
            else if (!string.IsNullOrEmpty(email))
                user = await _userManager.FindByEmailAsync(email);
            return new ApiResponse<UserApplication>()
            {
                Response = user,
                Success = user != null
            };
        }


        [HttpGet("Accounts")]
        public async Task<ApiResponse<List<AccountViewModel>>> Accounts()
        {
            var users = await _userManager.Users.Select(a => new AccountViewModel()
            {
                Email = a.Email,
                EmailConfirmed = a.EmailConfirmed,
                CreationTime = a.CreationTimeAt.ToString("G"),
                Gender = a.Gender.Gender,
                Id = a.Id,
                UserName = a.UserName
            }).ToListAsync();

            return new ApiResponse<List<AccountViewModel>>()
            {
                Response = users,
                Success = users != null,
            };
        }

    }
}
