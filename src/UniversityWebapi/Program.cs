
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityWebapi.Database;
using UniversityWebapi.Extensions;
using UniversityWebapi.Options;

namespace UniversityWebapi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.InitServices();
            builder.Services.InitRepositories();
            builder.Services.InitDatabase(builder.Configuration);
            builder.Services.InitAuth(builder.Configuration);
            builder.Services.InitSwagger();
            builder.Services.InitCors();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });
            builder.Services.AddOpenApi();
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<UniversityDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await dbContext.Database.MigrateAsync();
                if (!await roleManager.RoleExistsAsync(UserRoleDefaults.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoleDefaults.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoleDefaults.Student))
                    await roleManager.CreateAsync(new IdentityRole(UserRoleDefaults.Student));
                if (!await roleManager.RoleExistsAsync(UserRoleDefaults.Teacher))
                    await roleManager.CreateAsync(new IdentityRole(UserRoleDefaults.Teacher));
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.UseCors("University");

            app.Run();
        }
    }
}
