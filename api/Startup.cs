using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Entities.Interfaces;
using Api.Services;
using Api.Middleware;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            // Set the connection string from the environment variable
            if(Environment.GetEnvironmentVariable("DATABASE_URL") != null)
                Configuration["ConnectionStrings:DefaultConnection"] = Environment.GetEnvironmentVariable("DATABASE_URL");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure OpenTelemetry
            services.AddOpenTelemetry()
                .WithMetrics(builder => builder
                    .AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("LibraryServiceAPI"))
                );

            // Configure Entity Framework Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            // Add BookService as a scoped service
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
