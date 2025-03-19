using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using ZaalVpn.API;
using ZaalVpn.API.Entities;
using ZaalVpn.ViewModel;
using AppContext = ZaalVpn.API.AppContext;

var builder = WebApplication.CreateBuilder(args);

//var cs = builder.Configuration.GetConnectionString("DbKey1");
var cs = builder.Configuration.GetSection("dbInfo").Get<AppDb>();

// Add services to the container.

builder.Services.AddDbContext<AppContext>(op =>
{
    op.UseSqlServer(cs.ToString(), p =>
    {
        p.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);
    });
});

builder.Services.AddIdentity<UserApplication, RoleApplication>(op =>
{
    // Password
    op.Password.RequireLowercase = false;
    op.Password.RequireUppercase = false;
    op.Password.RequiredLength = 7;
    op.Password.RequireDigit = false;
    op.Password.RequireNonAlphanumeric = false;

    // User
    op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    op.User.RequireUniqueEmail = true;

    // Email 
    op.SignIn.RequireConfirmedEmail = false;
    op.SignIn.RequireConfirmedPhoneNumber = false;
    op.SignIn.RequireConfirmedAccount = false;

    // LockOut
    op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    op.Lockout.MaxFailedAccessAttempts = 5;
    op.Lockout.AllowedForNewUsers = true;


    // Stores
    op.Stores.ProtectPersonalData = false;

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppContext>();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitingPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();


builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(a =>
{
    a.AddPolicy(Policies.Admin, ac =>
    {
        ac.RequireRole(Roles.Admin);
    });
    a.AddPolicy(Policies.User, ac =>
    {
        ac.RequireRole(Roles.User);
    });
});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "_zaalVpn";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;

    //options.Cookie.SameSite = SameSiteMode.Strict;
    //options.Cookie.Domain = builder.Configuration.GetSection("domainName").Value;

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.WriteAsJsonAsync(new ResultModel().Set(HttpStatusCode.Forbidden)
            .Failed("مشکلی وجود دارد با مدیریت تماس بگیرید !"));
        return Task.CompletedTask;
    };
});


builder.Services.AddCors(a =>
{
    a.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<CheckAuthenticationMiddleware>();


app.MapControllers();

app.Run();
public class CheckAuthenticationMiddleware(RequestDelegate next)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //context.Response.Headers.Add("X-Role-Name","Brok");
            //Console.WriteLine($"Count : {context.Response.Headers.Count}");
            //Console.ForegroundColor = ConsoleColor.Blue;
            //foreach (var hd in context.Response.Headers)
            //{
            //    Console.WriteLine($"Key : {hd.Key} , Value : {hd.Value.ToString()}");
            //}
            //Console.ResetColor();

            //Console.ForegroundColor = ConsoleColor.Blue;

            //foreach (var cl in context.User.Claims)
            //{
            //    Console.WriteLine($"Key : {cl.Type} , Value : {cl.Value}");
            //}

            //Console.ResetColor();
            var mt = context.GetEndpoint()?.Metadata;

            if (mt?.GetMetadata<IAuthorizeData>() == null)
                if (mt?.GetMetadata<IAllowAnonymous>() != null)
                {
                    await next(context);
                    return;
                }


            if (context.User.Identity is { IsAuthenticated: false })
            {
                var baseResult = new ResultModel(false).Set(HttpStatusCode.Unauthorized)
                    .Failed("دسترسی وجود ندارد");

                await context.Response.WriteAsJsonAsync(baseResult, JsonSerializerOptions.Default);
                return;
            }


            await next(context);

        }
        catch (Exception e)
        {
            Console.WriteLine("EX : " + e.Message);
            //e.Message, e.InnerException is not null ? e.InnerException.Message : ""
            var res = new ResultModel(false, HttpStatusCode.BadRequest);
            res.Messages.Add(e.Message);
            res.Messages.Add(e.InnerException is not null ? e.InnerException.Message : "Oops...");
            await context.Response.WriteAsJsonAsync(res);

        }
    }
}