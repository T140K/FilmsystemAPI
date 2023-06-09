using FilmSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FilmSystemContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionstring")));
            builder.Services.AddCors(options =>
            options.AddPolicy("MyPolicy",
            builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                )
            );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FilmSystemContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}