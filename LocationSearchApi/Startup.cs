using BankPlatform.Api.Extenstions;
using BankPlatform.Api.Filters;
using Domain;
using LocationSearch.Repository;
using LocationSearch.Service;
using LocationSearch.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Interfaces;

namespace LocationSearchApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

            });
            services.AddDbContext<LocationDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocationSearchDB")));

            AddServiceConfiguration(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ILocationRepository), typeof(LocationRepository));
            services.AddScoped(typeof(ICsvService), typeof(CsvService));
            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureErrorHandler(logger);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void AddServiceConfiguration(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                       new BadRequestObjectResult(actionContext.ModelState);
            });
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(ValidationFilterAttribute));
            });
        }
    }
}
