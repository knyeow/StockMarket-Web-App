using Ocelot.Cache;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


namespace WebApiGateway.Api
{
    public class Program
    {
        public static  void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Ocelot
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            builder.Services.AddOcelot();

            //CORS Service
            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(options => {
                options.WithOrigins("http://localhost:3000").AllowAnyMethod();
                options.WithOrigins("http://localhost:3000").AllowAnyHeader();
            });

            app.UseAuthorization();



            app.MapControllers();

            app.UseOcelot().Wait();
            app.Run();
        }
    }
}