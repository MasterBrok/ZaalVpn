using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API;
using ZaalVpn.API.Entities;
using AppContext = ZaalVpn.API.AppContext;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DbKey1");

// Add services to the container.

builder.Services.AddDbContext<AppContext>(op =>
{
    op.UseSqlServer(cs);
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
    op.Lockout.AllowedForNewUsers = false;


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
