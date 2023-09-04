using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VisitorManagement.Application.MappingProfile;
using VisitorManagement.Application.Services;
using VisitorManagement.Infrastructure.Data;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("vmscorspolicy", p =>
                {
                    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            }
            );

            // Add services to the container.
            ConfigurationManager configuration = builder.Configuration;
            builder.Services.AddDbContext<VisitorManagementApplicationContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("VisitorManagementApplicationContext"));
            });

            // Add services to the container.
            builder.Services.AddScoped<IVisitorService, VisitorService>();

            builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
            builder.Services.AddAutoMapper(typeof(VisitorManagementAutoMapperProfile).Assembly);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("vmscorspolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}