using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebService.Repository;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<EnvironmentContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EnvironmentDatabase")));
            services.AddControllers();
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Sleep Monitoring API",
                        Description = "API for showing information related to the sleep monitoring system",
                        Version = "v2"
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v2/swagger.json", "Sleep monitoring");
            });
        }
    }
}
