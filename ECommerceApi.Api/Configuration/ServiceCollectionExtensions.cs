using System.Security.Claims;
using System.Text;
using ECommerceApi.Core;
using ECommerceApi.Data.Entities;
using ECommerceApi.Data.Options;
using ECommerceApi.Infrastructure;
using ECommerceApi.Infrastructure.Context;
using ECommerceApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddControllersAsServices();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerServices();


        return services;
    }

    public static IServiceCollection AddSecurityServices(
        this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var secrets = services.BuildServiceProvider().GetRequiredService<IOptions<SecretOptions>>().Value;
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = secrets.JWT.Issuer,
                    ValidAudience = secrets.JWT.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrets.JWT.Key)),
                    NameClaimType = ClaimTypes.NameIdentifier,
                };
            });
        
        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddDataServices(
        this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var secrets = services.BuildServiceProvider().GetRequiredService<IOptions<SecretOptions>>().Value;

            options.UseSqlServer(
                secrets.ConnectionStrings.DefaultConnection,
                sql => sql.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                    .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        return services;
    }

    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServiceDependencies(configuration)
            .AddCoreDependencies()
            .AddInfrastructureDependencies();
    }

    private static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce API",
                    Version = "v1"
                });

                // Enable Annotations (for [FromBody], [FromQuery], etc.)
                options.EnableAnnotations();
                // Add Swagger examples
                options.ExampleFilters();

                // Add Authorization header
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                    Enter 'Bearer' [space] and then your token in the text input below.
                                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            }
        );
        // Register Swagger example providers
        services.AddSwaggerExamplesFromAssemblyOf<Program>();
    }
}