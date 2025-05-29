using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UniversityWebapi.Database;
using UniversityWebapi.Models;
using UniversityWebapi.Options;
using UniversityWebapi.Repositories;
using UniversityWebapi.Services;

namespace UniversityWebapi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void InitServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<JwtTokenService>();
            serviceCollection.AddScoped<FacultyService>();
            serviceCollection.AddScoped<GroupService>();
            serviceCollection.AddScoped<StudentProfileService>();
            serviceCollection.AddScoped<MajorService>();
        }
        public static void InitRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<JwtTokenRepository>();
            serviceCollection.AddScoped<FacultyRepository>();
            serviceCollection.AddScoped<GroupRepository>();
            serviceCollection.AddScoped<StudentProfileRepository>();
            serviceCollection.AddScoped<MajorRepository>();
        }
        public static void InitDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<UniversityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            serviceCollection.AddIdentity<User, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<UniversityDbContext>();
        }
        public static void InitAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("Jwt");
            serviceCollection.Configure<JwtOptions>(jwtSection);
            
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                JwtOptions jwtOptions = jwtSection.Get<JwtOptions>() ?? throw new InvalidConfigurationException("Missing JWT options");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.KeyInBytes)
                };
            });
        }

        public static void InitSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Jwt Identity Service",
                    Version = "v1",
                });

                options.AddSecurityDefinition(
                    name: JwtBearerDefaults.AuthenticationScheme,
                    securityScheme: new OpenApiSecurityScheme()
                    {
                        Description = "Input yout JWT token here:",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                    });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme() {
                        Reference = new OpenApiReference() {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                }
                    }
                );
            });
        }

        public static void InitCors(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("University", policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}