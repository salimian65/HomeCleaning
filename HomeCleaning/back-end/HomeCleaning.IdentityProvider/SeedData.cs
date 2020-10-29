// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using HomeCleaning.Domain;
using HomeCleaning.Persistance;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HomeCleaning.IdentityProvider
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<HomeCleaningContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HomeCleaningContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<HomeCleaningContext>();
                    context.Database.Migrate();

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var customer = roleMgr.FindByNameAsync("customer").Result;
                    if (customer == null)
                    {
                        customer = new IdentityRole
                        {
                            Name = "customer"
                        };
                        _ = roleMgr.CreateAsync(customer).Result;
                    }

                    //------------------------------------------------------------------------
                    var server = roleMgr.FindByNameAsync("server").Result;
                    if (server == null)
                    {
                        server = new IdentityRole
                        {
                            Name = "server"
                        };
                        _ = roleMgr.CreateAsync(server).Result;
                    }

                    //------------------------------------------------------------------------
                    var admin = roleMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole
                        {
                            Name = "admin"
                        };
                        _ = roleMgr.CreateAsync(admin).Result;
                    }

                    ///////////////////////////////////////////////////////////////////
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var mehrdad = userMgr.FindByNameAsync("mehrdad").Result;
                    if (mehrdad == null)
                    {
                        mehrdad = new ApplicationUser
                        {
                            UserName = "mehrdad",
                            Email = "salimian.mehrdad@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(mehrdad, "Pass123$").Result;
                      
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        //alice = userMgr.FindByNameAsync("alice").Result;
                    
                        result = userMgr.AddClaimsAsync(mehrdad, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Mehrdad Salimian"),
                            new Claim(JwtClaimTypes.GivenName, "Mehrdad"),
                            new Claim(JwtClaimTypes.FamilyName, "Salimian"),
                            new Claim(JwtClaimTypes.WebSite, "http://Mehrdad.com"),
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(mehrdad, customer.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(mehrdad, customer.Name).Result;
                        }

                        Log.Debug("mehrdad created");
                    }
                    else
                    {
                        Log.Debug("mehrdad already exists");
                    }

                    //------------------------------------------------------------------------
                    var elham = userMgr.FindByNameAsync("elham").Result;
                    if (elham == null)
                    {
                        elham = new ApplicationUser
                        {
                            UserName = "elham",
                            Email ="elham@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(elham, "Pass123$").Result;
                     
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                       
                        //bob = userMgr.FindByNameAsync("bob").Result;
                        result = userMgr.AddClaimsAsync(elham, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Elham Shamouli"),
                            new Claim(JwtClaimTypes.GivenName, "Elham"),
                            new Claim(JwtClaimTypes.FamilyName, "Shamouli"),
                            new Claim(JwtClaimTypes.WebSite, "http://Elham.com"),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(elham, server.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(elham, server.Name).Result;
                        }

                        Log.Debug("elham created");
                    }
                    else
                    {
                        Log.Debug("elham already exists");
                    }

                    //------------------------------------------------------------------------
                    var behcet = userMgr.FindByNameAsync("behcet").Result;
                    if (behcet == null)
                    {
                        behcet = new ApplicationUser
                        {
                            UserName = "behcet",
                            Email = "behcet@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(behcet, "Pass123$").Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        //bob = userMgr.FindByNameAsync("bob").Result;
                        result = userMgr.AddClaimsAsync(behcet, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Elham Shamouli"),
                            new Claim(JwtClaimTypes.GivenName, "Elham"),
                            new Claim(JwtClaimTypes.FamilyName, "Shamouli"),
                            new Claim(JwtClaimTypes.WebSite, "http://Elham.com"),
                            new Claim("location", "somewhere")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(behcet, admin.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(behcet, admin.Name).Result;
                        }

                        Log.Debug("behcet created");
                    }
                    else
                    {
                        Log.Debug("behcet already exists");
                    }
                }
            }
        }
    }
}


//var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//IdentityResult roleResult;
////Adding Admin Role
//var roleCheck = RoleManager.RoleExistsAsync("Admin").Result;
//if (!roleCheck)
//{

//IdentityRole adminRole = new IdentityRole("Admin");
////create the roles and seed them to the database
//roleResult = RoleManager.CreateAsync(adminRole).Result;

//RoleManager.AddClaimAsync(adminRole, new Claim(ClaimTypes.AuthorizationDecision, "edit.post")).Wait();
//RoleManager.AddClaimAsync(adminRole, new Claim(ClaimTypes.AuthorizationDecision, "delete.post")).Wait();

//ApplicationUser user = new ApplicationUser { UserName = "v-nany@hotmail.com", Email = "v-nany@hotmail.com" };
//UserManager.CreateAsync(user, "Pass123$").Wait();

//_ = UserManager.AddToRoleAsync(user, adminRole.Name).Result;
//}