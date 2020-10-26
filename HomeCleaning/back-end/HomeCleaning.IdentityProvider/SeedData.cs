// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using HomeCleaning.IdentityProvider.Data;
using HomeCleaning.IdentityProvider.Models;
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
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
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

                    var admin = roleMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole
                        {
                            Name = "admin"
                        };
                        _ = roleMgr.CreateAsync(admin).Result;
                    }

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            UserName = "alice",
                            Email = "AliceSmith@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                      
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        //alice = userMgr.FindByNameAsync("alice").Result;
                    
                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(alice, customer.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(alice, customer.Name).Result;
                        }

                        Log.Debug("alice created");
                    }
                    else
                    {
                        Log.Debug("alice already exists");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                     
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                       
                        //bob = userMgr.FindByNameAsync("bob").Result;
                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(bob, admin.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(bob, admin.Name).Result;
                        }

                        Log.Debug("bob created");
                    }
                    else
                    {
                        Log.Debug("bob already exists");
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