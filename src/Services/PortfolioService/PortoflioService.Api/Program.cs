
using Microsoft.EntityFrameworkCore;
using PortoflioService.Api.Managers;
using PortoflioService.Api.Repostories.PortfolioReps;
using PortoflioService.Api.Services;

namespace PortoflioService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Database Connections
            builder.Services.AddDbContext<PortfolioContext>
               (options => options.UseSqlServer
               (builder.Configuration.GetConnectionString("PortfolioDbConnection")));

            //Stock Scops
            builder.Services.AddScoped<PortfolioManager>()
                .AddScoped<StockPositionManager>()
                .AddScoped<StockOperationService>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}