using System;
using System.Linq;
using HomeCleaning.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;

namespace HomeCleaning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
     
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
      
        public void ConfigureServices(IServiceCollection services)
        {
            var serilogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            try
            {
                services.AddSingleton(new LoggerFactory().AddSerilog(serilogLogger).CreateLogger("TseCamWebApi"));
                services.AddHealthChecks(); 
                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TseCam Web API", Version = "v1" });
                    c.ResolveConflictingActions(d => d.First()); // until aspnetcore supports action resolver
                });

               // services.AddControllers();
                services.AddDbContext<HomeCleaningContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("HomeCleaningContext")));

                services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                        builder =>
                        {
                            builder.WithOrigins("*","http://localhost:8080");
                        });
                });

                new Bootstrap(services, Configuration).WireUp();
            }
            catch (Exception e)
            {
                Log.Error($"This error occured in startup in  ConfigureServices {e.Message}");
                throw;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHealthChecks("/health");
                app.UseRouting();
                app.UseCors(MyAllowSpecificOrigins);
                app.UseHttpMetrics();
                app.UseAuthentication();
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
                    endpoints.MapMetrics();
                });
            }
            catch (Exception e)
            {
                Log.Error($"This error occured in startup in  ConfigureServices {e.Message}");
                throw;
            }

        }
    }
}
