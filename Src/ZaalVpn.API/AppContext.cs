using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZaalVpn.API.Entities;

namespace ZaalVpn.API
{
    public class AppContext : IdentityDbContext<UserApplication, RoleApplication, string, UserClaimApplication, UserRoleApplication, UserLoginApplication, RoleClaimApplication, UserTokenApplication>
    {
        public AppContext(DbContextOptions<AppContext> option) : base(option)
        {

        }



        public DbSet<ServerEntity> Servers { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<ConfigEntity> Configs { get; set; }
        public DbSet<GenderEntity> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.HasDefaultSchema("auth");
            builder.Entity<UserApplication>(a =>
            {
                a.Ignore(r => r.PhoneNumber);
                a.Ignore(r => r.PhoneNumberConfirmed);
                a.Ignore(r => r.TwoFactorEnabled);
            });

            builder.Entity<UserApplication>().ToTable("tbUsers").Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<RoleApplication>().ToTable("tbRoles").Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<RoleClaimApplication>().ToTable("tbRoleClaims").Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<UserClaimApplication>().ToTable("tbUserClaim").Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<UserLoginApplication>().ToTable("tbUserLogins");
            builder.Entity<UserRoleApplication>().ToTable("tbUserRoles");
            builder.Entity<UserTokenApplication>().ToTable("tbUserTokens");
            builder.Entity<GenderEntity>().ToTable("tbGenders");

            builder.Entity<ServerEntity>().ToTable("tbServers","vpn");
            builder.Entity<ConfigEntity>().ToTable("tbConfigs", "vpn");
            builder.Entity<CountryEntity>().ToTable("tbCountries", "vpn");


            builder.Entity<UserApplication>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.Role)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                //b.HasOne(e => e.Gender)
                //    .WithOne(e => e.User)
                //    .HasForeignKey<UserApplication>(e => e.GenderId);
            });

            builder.Entity<RoleApplication>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });




            builder.Entity<ServerEntity>()
                .HasOne(a => a.Country)
                .WithMany(a => a.Servers)
                .HasForeignKey(a=>a.CountryId);


            builder.Entity<ServerEntity>()
                .HasMany(d => d.Configs)
                .WithOne(d => d.Server)
                .HasForeignKey(d => d.ServerId);



            var roles = new List<RoleApplication>()
            {
                new()
                {
                    Name = API.Roles.Admin
                },
                new()
                {
                    Name = API.Roles.User
                },
            };

            builder.Entity<RoleApplication>()
                .HasData(roles);

            var genders = new List<GenderEntity>()
            {
                new("Male"),
                new("Female"),
                new("NonBinary"),
                new("Bigender"),
                new("Agender"),
                new("Feminine"),
                new("Androgynous"),
                new("Other")
            };

            builder.Entity<GenderEntity>()
                .HasData(genders);

            var user = new UserApplication()
            {
                UserName = "brok",
                Email = "brok@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                GenderId = genders[0].Id,
                ShortId = "111111"
            };

            user.PasswordHash = new PasswordHasher<UserApplication>().HashPassword(user, "#admin_zaal@com");

            var usroles = new List<UserRoleApplication>()
            {
                new()
                {
                    UserId = user.Id,
                    RoleId = roles[0].Id
                },
                new()
                {
                    UserId = user.Id,
                    RoleId = roles[1].Id
                },
            };


            builder.Entity<UserApplication>().HasData(user);

            builder.Entity<UserRoleApplication>().HasData(usroles);


        }


    }
}
