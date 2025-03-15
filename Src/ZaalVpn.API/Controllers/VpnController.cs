using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API.Entities;
using ZaalVpn.ViewModel;

namespace ZaalVpn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VpnController : ControllerBase
    {

        private ResultModel result = new();
        private readonly AppContext _appContext;

        public VpnController(AppContext appContext)
        {
            _appContext = appContext;
        }


        [HttpPost("NewServer")]
        public async Task<ResultModel> AddServer([FromBody] AddServerViewModel server)
        {
            var entity = new ServerEntity(server.CountryId, server.IsActive);

            if (server.ConfigServers is not null)
                entity.AddRangeConfigs(server
                    .ConfigServers
                    .Select(a => new ConfigEntity(a.Config, a.IsActive, entity.Id)).ToList());

            _appContext.Servers.Add(entity);
            var save = await _appContext.SaveChangesAsync();
            return save > 0 ? result.Succeeded() : result.Failed(OperationMessage.Database);
        }

        [HttpPut("UpdateServer")]
        public async Task<ResultModel> EditServer([FromBody] EditServerViewModel server)
        {
            var find = await _appContext
                .Servers
                .Include(r => r.Configs)
                .Include(a => a.Country)
                .FirstOrDefaultAsync(a => a.Id == server.Id);

            if (find is null)
                return result.NotFound();

            if (server.ConfigServers != null)
                find.AddRangeConfigs(server
                    .ConfigServers
                    .Select(a => new ConfigEntity(a.Config, a.IsActive, find.Id)).ToList());

            find.Edit(server.CountryId, server.IsActive);
            var save = await _appContext.SaveChangesAsync();
            return save > 0 ? result.Succeeded() : result.Failed(OperationMessage.Database);
        }


        [HttpGet("Servers")]
        public async Task<ApiResponse<List<ServerViewModel>>> Servers()
        {
            var result = new ApiResponse<List<ServerViewModel>>();
            var data = await _appContext
                 .Servers
                 .AsNoTracking()
                 .Include(a => a.Country)
                 .Include(a => a.Configs)
                 .Where(a => a.IsActive)
                 .OrderBy(a => a.CountryId)
                 .ToListAsync();


            if (data is null || data.Count == 0)
                return result.Set(HttpStatusCode.BadRequest).FailedErrors(OperationMessage.NoServers);

            var servers = data.Select(s => new ServerViewModel()
            {
                Id = s.Id,
                Country = s.Country.Name,
                Configs = s.Configs.Select(a => new ConfigViewModel()
                {
                    Name = a.Config,
                    Id = a.Id
                }).ToList()
            }).ToList();

            result.Response = servers;
            return result.Succeeded();
        }


        [HttpGet("GetConfig")]
        public async Task<ApiResponse<FullConfigViewMode>> GetConfig([FromQuery] string id)
        {
            var result = new ApiResponse<FullConfigViewMode>();

            var data = await _appContext.Configs.FirstOrDefaultAsync(a => a.Id == id);
            if (data is null)
                return result.NotFound();

            result.Response = new FullConfigViewMode()
            {
                Config = data.Config
            };
            return result.Succeeded();
        }
    }
}
