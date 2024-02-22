
using Microsoft.Extensions.Options;

namespace PdfExporter.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding cors configration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                     builder =>
                     {
                         builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                     });
            });


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("AllowOrigin");
                app.UseCors("AllowAnyOrigin");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();

            app.UseAuthorization();

   
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
