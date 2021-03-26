using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SGM.SAC.Domain.Dto;
using SGM.SAC.Domain.QuerySide.Queries;
using SGM.SAC.Domain.QuerySide.QueryHandlers;
using SGM.SAC.Domain.Settings;
using System;
using System.Text;

namespace SGM.SAC.Infra.Crosscutting.Bootstrappings
{
    public class ServicesBootStrap
    {
        protected ServicesBootStrap() { }

        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            // MediatR
            services.AddMediatR(typeof(ServicesBootStrap).Assembly);

            // Settings
            var authSettingsSection = config.GetSection("AuthSettings");
            services.Configure<AuthSettings>(authSettingsSection);
            var authSettings = authSettingsSection.Get<AuthSettings>();
            var key = Encoding.ASCII.GetBytes(authSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
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

            // Query Handlers
            services.AddHttpClient<IRequestHandler<PropertyTaxQuery, PropertyTaxResult>, PropertyTaxQueryHandler>(client =>
            {
                client.BaseAddress = new Uri(config.GetSection("BaseAddress").Value);
            });
        }
    }
}
