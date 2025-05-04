using Diabetes.Core.Entities;
using Diabetes.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Diabetes.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Diabetes.Repository;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Diabetes.Core.Interfaces;
using Diabetes.Services;
using Diabetes.Repository.Repositories;


namespace Diabetes.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure DbContext
            builder.Services.AddDbContext<StoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            builder.Services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<IdentityRole>>()
            .AddEntityFrameworkStores<StoreContext>()
            .AddDefaultTokenProviders();

            // Configure JWT Authentication
            var tokenKey = builder.Configuration["JwtSettings:TokenKey"] ??
                          builder.Configuration["TokenKey"];

            // Fallback for development only
            if (string.IsNullOrEmpty(tokenKey))
            {
                if (builder.Environment.IsDevelopment())
                {
                    tokenKey = "development-key-32-characters-long-123456";
                    Console.WriteLine($"Using development token key: {tokenKey}");
                }
                else
                {
                    throw new Exception("JWT TokenKey is missing in configuration");
                }
            }

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                
            });
            
            // Configure Authorization
            builder.Services.AddAuthorization(options =>
            {
                var roles = new[] {
                    "CasualUser", "Doctor", "Clerk",
                    "MedicalSyndicate", "NationalInstitute", "MinistryOfHealth",
                    "WorldHealthOrganization", "GeneralAuthority", "Manager", "Admin"
                };

                foreach (var role in roles)
                {
                    options.AddPolicy(role, policy => policy.RequireRole(role));
                }
            });

            // Register Services
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<ICreatePostService, CreatePostService>();
            




            #endregion

            var app = builder.Build();

            #region Configure Middleware Pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            #region Seed Data

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(context);
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await RoleSeeder.SeedRolesAsync(services);
                    await SystemUserSeeder.SeedUsersAsync(services);
                    await ActorSeeder.SeedActorsAsync(services);

                    Console.WriteLine("Database seeding completed successfully");
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred during migration or seeding");
                }
            }

            #endregion

            await app.RunAsync();
        }
    }
}