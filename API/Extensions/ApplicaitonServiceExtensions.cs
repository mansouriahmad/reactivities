using Application.Activities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddDbContext<DataContext>(opt =>
      {
        opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
      });


      services.AddCors(options =>
        {
          options.AddPolicy("CorsPolicy", policy =>
          {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
          });


        }
      );

      services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
      services.AddAutoMapper(typeof(MappingProfiles).Assembly);

      return services;
    }
  }
}