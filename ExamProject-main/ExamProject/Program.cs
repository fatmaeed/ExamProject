using ExamProject.Application.Interfaces.IServices;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Application.MappingConfig;
using ExamProject.Application.Services;
using ExamProject.Domain.Entities;
using ExamProject.Domain.Expections;
using ExamProject.Infrastructure.Data;
using ExamProject.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExamProject {

    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IUserExamService, UserExamService>();
            builder.Services.AddAutoMapper(typeof(AdminMapping));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(
              Options => {
                  Options.Password.RequireNonAlphanumeric = false;
                  Options.Password.RequireNonAlphanumeric = true;
                  Options.Password.RequireLowercase = true;
                  Options.Password.RequireUppercase = true;
                  Options.Password.RequireDigit = true;
              }).AddEntityFrameworkStores<ExamDbContext>().AddDefaultTokenProviders();

            builder.Services.AddDbContext<ExamDbContext>(
                Options => Options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ExamDbConnection"))
                );
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            builder.Services.AddAuthentication(opt => opt.DefaultAuthenticateScheme = "defSheme")
                .AddJwtBearer("defSheme", op => {
                    op.TokenValidationParameters = new TokenValidationParameters() {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]))
                    };
                });
            builder.Services.AddAutoMapper(typeof(AuthenticationMapping), typeof(AdminMapping), typeof(StudentMapping));
            // Add services to the container.
            builder.Services.AddScoped<IExamService, ExamService>();

            builder.Services.AddControllers();
            builder.Services.AddCors(
                (op) => op.AddPolicy("AllowAll", (policy) => {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                })
                );
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.MapOpenApi();
                app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", "v1"); });

                app.UseSwaggerUI((op) => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}