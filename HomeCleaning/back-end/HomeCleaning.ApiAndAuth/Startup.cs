// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Globalization;
using System.Linq;
using HomeCleaning.ApiAndAuth.Authorization;
using IdentityServer4;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace HomeCleaning.ApiAndAuth
{
    public class Startup
    {
        private const string CorsAllowUIApp = "corsAllowUIApp";
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

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

            //-----------------------------------------------------------------------

            services.AddControllersWithViews();

            services.AddDbContext<HomeCleaningContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HomeCleaningContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<HomeCleaningContext>()
                .AddDefaultTokenProviders();

            var config = new Config(Configuration);
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddInMemoryApiScopes(config.ApiScopes)
                .AddInMemoryClients(config.GetClients())
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>();

                 services.AddLocalApiAuthentication();

            // services.AddScoped<IProfileService, ProfileService>();

            builder.Services.ConfigureExternalCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = (SameSiteMode)(-1); //SameSiteMode.Unspecified in .NET Core 3.1
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = (SameSiteMode)(-1); //SameSiteMode.Unspecified in .NET Core 3.1
            });



            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "717469225962-3vk00r8tglnbts1cgc4j1afqb358o8nj.apps.googleusercontent.com";
                    options.ClientSecret = "babQzWPLGwfOQVi0EYR-7Fbb";
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                });



            //.............................................
            services.AddCors(options =>
            {
                options.AddPolicy(CorsAllowUIApp,
                    policyBuilder =>
                    {
                        policyBuilder.WithOrigins(Configuration["partner:webClient"]).AllowAnyMethod().AllowAnyHeader();
                    });
            });

            //services.AddAuthentication("Bearer")
            //    .AddJwtBearer("Bearer", options => {
            //        options.Authority = Configuration["partner:authService"];
            //        options.RequireHttpsMetadata = false;

            //        options.Audience = "backend";
            //    });

            services.AddMvc(options => { options.Filters.Add<UnhandledExceptionFilterAttribute>(); })
                .AddControllersAsServices();

            services.AddAuthorization(options => {
                options.AddPolicy("ProductOwner", policy => policy.Requirements.Add(new OrderOwnerAuthorizationRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, OrderOwnerAuthorizationHandler>();

            new Bootstrap(services, Configuration).WireUp();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseCors(CorsAllowUIApp);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}