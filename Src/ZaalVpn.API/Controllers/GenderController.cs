using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.ViewModel;

namespace ZaalVpn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {

        private readonly AppContext _context;

        public GenderController(AppContext context)
        {
            _context = context;
        }



        [HttpGet("Genders")]
        [AllowAnonymous]
        public async Task<ApiResponse<Dictionary<string, string>>> Genders()
        {
            var genders = await
                _context.Genders.ToDictionaryAsync(a => a.Id, a => a.Gender);
            return new ApiResponse<Dictionary<string, string>>()
            {
                Response = genders,
                Success = genders != null,
                HttpCode = HttpStatusCode.OK
            };
        }


    }
}
