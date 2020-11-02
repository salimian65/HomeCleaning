using Framework.Domain;
using HomeCleaning.Api.Contexts;
using HomeCleaning.Persistance.DataAccess;
using HomeCleaning.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeCleaning.Api
{
    public class Bootstrap
    {
        private readonly IServiceCollection services;
     
        public Bootstrap(IServiceCollection services, IConfiguration configuration)
        {
            this.services = services;
        }

        public void WireUp()
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserContext, UserContext>();
         //   services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
         //   services.AddScoped<IIdpUserManagementService, KeyCloakUserManagementService>();
         //   services.AddScoped<UserService>();
        }
    }
}