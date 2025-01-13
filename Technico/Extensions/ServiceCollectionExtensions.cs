using Technico.Interfaces;
using Technico.Repositories;
using Technico.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Technico.Mappers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTechnicoServices(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

        // Add Controllers
        services.AddControllers();

        // Add Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        // Add Repositories
        services.AddScoped<UserRepository>();
        services.AddScoped<PropertyRepository>();
        services.AddScoped<RepairRepository>();

        // Add Interfaces and Implementations
        services.AddScoped<IMapper, Mapper>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IRepairService, RepairService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IRepairRepository, RepairRepository>();

        // Add DbContext


        // Add CORS Policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}
