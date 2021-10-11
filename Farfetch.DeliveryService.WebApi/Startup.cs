using Farfetch.DeliveryService.Migrator;
using Farfetch.DeliveryService.Models;
using Farfetch.DeliveryService.Repositories;
using Farfetch.DeliveryService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Farfetch.DeliveryService.WebApi
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
            var FarfetchConfiguration = new Models.FarfetchConfiguration { ConnectionString = Configuration["Farfetch:ConnectionString"] };

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "deliveryservice";
                });

            //Setting Admin role
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", builder =>
                {
                    builder.RequireRole(new[] { "Admin" });
                });
            });

            new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables()
              .Build();


            services
                .AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Farfetch Delivery Service Web Api", Version = "v1" }));

            //Injections
            services.AddTransient<IFarfetchConfiguration, FarfetchConfiguration>();
            services.AddTransient<IPointProvider, PointProvider>();
            services.AddTransient<IRouteProvider, RouteProvider>();
            services.AddTransient<IPointRepository, PointRepository>();
            services.AddTransient<IRouteRepository, RouteRepository>();
            services.AddTransient<IDeliveryServiceProvider, DeliveryServiceProvider>();
            services.AddSingleton<IFarfetchConfiguration>(FarfetchConfiguration);

            //Migration
            var db = services.AddDbContext<FarfetchDbContext>(opts =>
            opts.UseSqlServer(FarfetchConfiguration.ConnectionString,
            options => options.MigrationsAssembly("Farfetch.DeliveryService.Migrator")));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            //Migrating to latest
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<FarfetchDbContext>();
                context.Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery Service v1"));
        }
    }
}
