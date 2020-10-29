// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using HomeCleaning.Persistance.Services;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeCleaning.IdentityProvider
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<HomeCleaningContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HomeCleaningContext>()
                .AddDefaultTokenProviders();

            var config = new Config(Configuration);

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                 // .AddInMemoryApiResources(config.GetApis())
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();


            services.AddScoped<IProfileService, ProfileService>();

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

            //var rsa = new RsaKeyService(Environment, TimeSpan.FromDays(30));
            //services.AddSingleton<RsaKeyService>(provider => rsa);
            //if (Environment.IsDevelopment())
            //{
            builder.AddDeveloperSigningCredential();
            //}
            //else
            //{
            //    builder.AddSigningCredential(rsa.GetKey());
            //   // throw new Exception("need to configure key material");
            //}

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to http://localhost:5000/signin-google
                   // options.ClientId = "copy client ID from Google here";
                   // options.ClientSecret = "copy client secret from Google here";
                   options.ClientId = "717469225962-3vk00r8tglnbts1cgc4j1afqb358o8nj.apps.googleusercontent.com";
                   options.ClientSecret = "babQzWPLGwfOQVi0EYR-7Fbb";
                   options.SignInScheme = IdentityConstants.ExternalScheme;
                });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}