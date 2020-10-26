using System;
using System.Globalization;
using System.Linq;
using HomeCleaning.Authorization;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;

namespace HomeCleaning
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
            var serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddSingleton(new LoggerFactory().AddSerilog(serilogLogger).CreateLogger("TseCamWebApi"));

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(o =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("tr"),
                    new CultureInfo("ar")
                };

                o.DefaultRequestCulture = new RequestCulture("en", "en");
                o.SupportedCultures = supportedCultures;
                o.SupportedUICultures = supportedCultures;

                o.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
            });

            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Home Cleaning Web API", Version = "v1" });
                c.ResolveConflictingActions(d => d.First()); // until aspnetcore supports action resolver
            });


            services.AddDbContext<HomeCleaningContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HomeCleaningContext")));

            services.AddCors(options =>
            {
                options.AddPolicy(CorsAllowUIApp,
                    policyBuilder =>
                    {
                       policyBuilder.WithOrigins(Configuration["partner:webClient"]).AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options => {
                    options.Authority = Configuration["partner:authService"];
                    options.RequireHttpsMetadata = false;

                    options.Audience = "backend";
                });

            services.AddAuthorization(options => {
                options.AddPolicy("ProductOwner", policy => policy.Requirements.Add(new OrderOwnerAuthorizationRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, OrderOwnerAuthorizationHandler>();

            new Bootstrap(services, Configuration).WireUp();

            services.AddMvc(options => { options.Filters.Add<UnhandledExceptionFilterAttribute>(); })
                .AddControllersAsServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
            app.UseStaticFiles();
            app.UseRouting(); 
            app.UseHttpMetrics();
            app.UseCors(CorsAllowUIApp);

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TseCam Web API");
            });

            //app.UseHttpsRedirection();
            //app.UseResultExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.Authority = Configuration["Jwt:Authority"];
                o.Audience = Configuration["Jwt:Audience"];

                //o.Events = new JwtBearerEvents()
                //{
                //    // OnTokenValidated = async ctx =>
                //    // {
                //    // 	//Add claim if they are
                //    // 	var claims = new List<Claim>
                //    // 	{
                //    // 		new Claim(ClaimTypes.Role, "superadmin")
                //    // 	};
                //    // 	var appIdentity = new ClaimsIdentity(claims);
                //    // 	ctx.Principal.AddIdentity(appIdentity);
                //    // },
                //    OnAuthenticationFailed = c =>
                //    {
                //        c.NoResult();

                //        c.Response.StatusCode = 500;
                //        c.Response.ContentType = "text/plain";
                //        //                        if (Environment.IsDevelopment())
                //        //                        {
                //        return c.Response.WriteAsync(c.Exception.ToString());
                //        //                        }
                //        //
                //        //                        return c.Response.WriteAsync("An error occured processing your authentication.");
                //    }
                //};
            });
        }
    }
}
