﻿using System.Text;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Granny.Api.Security.Configuration;
using Granny.Repository.Security;
using Granny.Repository.Security.Mongo;
using Granny.Services;
using Granny.Services.Interfaces;
using Granny.Util.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Granny.Api.Securirty.Setup
{
    public static class ServiceCollectionSetup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<Security.Services.IAuthenticationService, Security.Services.AuthenticationService>()
                .AddScoped<IUserServices, UserServices>();
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserRepository, UserRepository>(); ;
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

        public static IServiceCollection ConfigureMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return
                 services.Configure<GrannySecurityDatabaseSettings>(configuration.GetSection(nameof(GrannySecurityDatabaseSettings)))
                 .AddSingleton<IGrannySecurityDatabaseSettings>(sp => sp.GetRequiredService<IOptions<GrannySecurityDatabaseSettings>>().Value);
        }
    }
}
