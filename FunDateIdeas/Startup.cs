using FunDateIdeas.DAL.Data;
using FunDateIdeas.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunDateIdeas
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.AddControllersWithViews();
            services.AddDbContext<FunDateIdeasDbContext>(config =>
            {
                config.UseSqlServer(_configuration.GetConnectionString("FunDateIdeasDb"));
            });
            services.AddDbContext<FunDateIdeasIdentityDbContext>(config =>
            {
                config.UseSqlServer(_configuration.GetConnectionString("FunDateIdeasDb"));
            });

            services.AddIdentity<FunDateIdeasUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<FunDateIdeasIdentityDbContext>();

            services.Configure<IdentityOptions>(config =>
            {
                config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            });
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "FunDateIdeasIdentity.Cookie";
                config.LoginPath = "/Accounts/SignIn";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
