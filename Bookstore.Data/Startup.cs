using Bookstore.Data.AppContext;
using Bookstore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.Data
{
    public static class Startup
    {
        public static void InitData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<User, Role>(config =>
                    {
                        config.Password.RequireDigit = false;
                        config.Password.RequireLowercase = false;
                        config.Password.RequireNonAlphanumeric = false;
                        config.Password.RequireUppercase = false;
                        config.Password.RequiredLength = 5;

                        config.User.RequireUniqueEmail = true;
                    })
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.Scan(scan => scan.FromCallingAssembly()
                                      .AddClasses()
                                      .AsMatchingInterface());
        }
    }
}
