using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Granny.Api.Query.ConfigurationMapper;
using Granny.DAO.EntitiesRepository;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork;
using Granny.DAO.UnitOfWork.Interface;
using Granny.Services;
using Granny.Services.Interfaces;
using Granny.Util.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Granny.Api.Query.Setup
{
    public static class ServiceCollectionSetup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ILocationServices, LocationServices>()
                .AddScoped<IPriceServices, PriceServices>()
                .AddScoped<IProductServices, ProductServices>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<ILocationRepository, LocationRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IPriceRepository, PriceRepository>();
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddCollectionMappers();
            });
            var mapper = mappingConfig.CreateMapper();
            return services.AddSingleton(mapper);
        }

        public static AuthenticationBuilder ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            return services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
