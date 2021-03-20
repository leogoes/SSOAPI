using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SSOAPI.API.Extensions;
using SSOAPI.Infra.Data.Context;
using SSOAPI.Infra.Data.Models;

namespace SSOAPI.API.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SSOContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SSOContext>()
                .AddErrorDescriber<IdentityMensagens>()
                .AddDefaultTokenProviders();

            //JWT
            var appSettings = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            var appSettingsSecret = appSettings.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettingsSecret.Secret);

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = true;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettingsSecret.ValidoEm,
                    ValidIssuer = appSettingsSecret.Emissor
                };
            });

            return services;
        }
    }
}
