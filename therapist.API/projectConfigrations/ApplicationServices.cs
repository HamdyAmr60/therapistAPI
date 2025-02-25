using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Therapist.Core;
using Therapist.Core.Models.Identity;
using Therapist.Core.Services;
using Therapist.Reposatories;
using Therapist.Reposatories.Data.Identity;
using Therapist.Services;

namespace therapist.API.projectConfigrations
{
    public static class ApplicationServices
    {
        public static WebApplicationBuilder ApplicationService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connect = builder.Configuration.GetConnectionString("redis");
                return ConnectionMultiplexer.Connect(connect);
            });
            builder.Services.AddSingleton<ICacheService, CacheService>();
          builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Aud"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                    };
                });
            return builder;
        }
    }
}
