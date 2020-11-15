using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using HomeCleaning.Api.Authorization;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.DataAccess;
using HomeCleaning.Persistance.IdentityPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;

namespace HomeCleaning.Api
{
    public class Startup
    {
        private const string CorsAllowUIApp = "corsAllowUIApp";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //-----------------------------------------------------------------
            services.AddDbContext<HomeCleaningContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HomeCleaningContext")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<HomeCleaningContext>()
                .AddDefaultTokenProviders();

            //----------------------------------------------------------------------------
            //  (new JwtSecurityTokenHandler()).InboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["partner:authService"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "backend";
                });

            //----------------------------------------------------------------------------
            services.AddCors(options =>
            {
                options.AddPolicy(CorsAllowUIApp,
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins(Configuration["partner:webClient"]).AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("customer", policy => policy.RequireClaim("role", "customer"));
                // options.AddPolicy("server", policy => policy.RequireClaim("Role", "server"));
                // options.AddPolicy("admin", policy => policy.RequireClaim("Role", "admin"));
                options.AddPolicy("ProductOwner", policy => policy.Requirements.Add(new OrderOwnerAuthorizationRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, OrderOwnerAuthorizationHandler>();

            //----------------------------------------------------------------------------

            services.AddMvc(options => { options.Filters.Add<UnhandledExceptionFilterAttribute>(); })
                .AddControllersAsServices();

            new Bootstrap(services, Configuration).WireUp();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsAllowUIApp);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
