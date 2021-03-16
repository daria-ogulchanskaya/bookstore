using System;
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Bookstore.Core.Common;
using Bookstore.Core.Mappers;
using Bookstore.Core.Providers;
using Bookstore.Data;
using Bookstore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Bookstore.Core
{
    public static class Startup
    {
        public static void InitCore(this IServiceCollection services, IConfiguration configuration)
        {
            Type[] types = {
                typeof(JwtProvider),
                typeof(EmailProvider),
                typeof(CurrencyConverter),
                typeof(PasswordGenerator),
                typeof(SignInManager<User>),
                typeof(UserManager<User>)
            };

            services.Scan(scan =>
                scan.AddTypes(types)
                    .AsSelf()
                    .WithTransientLifetime());

            services.Scan(scan =>
                scan.FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var mapperConfig = new MapperConfiguration(configuration =>
            {
                configuration.EnableEnumMappingValidation();

                configuration.AddProfile(new OrderMapper());
                configuration.AddProfile(new PaymentMapper());
                configuration.AddProfile(new UserMapper());
                configuration.AddProfile(new PrintingEditionMapper());
                configuration.AddProfile(new AuthorMapper());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.InitData(configuration);
        }
    }
}
