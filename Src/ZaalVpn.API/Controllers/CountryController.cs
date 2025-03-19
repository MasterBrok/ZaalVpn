using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API.Entities;
using ZaalVpn.ViewModel;

namespace ZaalVpn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policies.Admin)]
    public class CountryController : ControllerBase
    {

        private ResultModel result = new();
        private readonly AppContext _appContext;

        public CountryController(AppContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet("Countries")]
        public async Task<ApiResponse<List<CountryViewModel>>> Countries()
        {
            var result = new ApiResponse<List<CountryViewModel>>();


            var countries = await _appContext.Countries.Select(a => new CountryViewModel()
            {
                Id = a.Id,
                CountryName = a.Name
            }).ToListAsync();

            if (countries is null || countries.Count == 0)
                return result.Set(HttpStatusCode.BadRequest).FailedErrors(OperationMessage.Null);

            result.Response = countries;
            return result.Succeeded();

        }


        [HttpPost("AddAcountry")]
        public async Task<ResultModel> AddCountry(AddCountryViewModel country)
        {
            _appContext.Countries.Add(new CountryEntity(country.Name));
            return await _appContext.SaveChangesAsync() > 0 ? result.Succeeded() : result.Set(HttpStatusCode.BadRequest).Failed(OperationMessage.NoServers);
        }
        [HttpDelete("DeleteCountry")]
        public async Task<ResultModel> DeleteCountry(string id)
        {
            var remove = await _appContext
                .Countries.
                Where(d => d.Id == id)
                .ExecuteDeleteAsync();
            return remove > 0 ? result.Succeeded() : result.Failed();
        }

        [HttpPut("EditCountry")]
        public async Task<ResultModel> EditCountry(EditCountryViewModel country)
        {
            var update = await _appContext.
                Countries
                .Where(a => a.Id == country.Id)
                .ExecuteUpdateAsync(a => a.SetProperty(d => d.Name, country.Name));
            return update > 0 ? result.Succeeded() : result.Failed();
        }

    }
}
