
using JobApplication.Application.Interfaces;
using JobApplication.Application.Services;
using JobApplication.Infrastructure.Persistence;
using JobApplication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar;
using Scalar.AspNetCore;
using System.Text;
using static JobApplication.Application.Services.AppService;

namespace JobApplication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string"
                + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<JobService>();
            builder.Services.AddScoped<IJobRepository,JobRepository>();

            builder.Services.AddScoped<IAppRepository, AppRepository>();
            builder.Services.AddScoped<ApplicationService>();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(JobApplication.Application.AssemblyReference).Assembly));


            builder.Services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", options =>
              {
           options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
              )
           };
    });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();





            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
