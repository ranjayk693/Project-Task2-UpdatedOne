
using Microsoft.Extensions.Options;

namespace PdfExporter.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Adding cors configration to avoid cors error
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

            
          

            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           
            //app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection(); 
            app.UseCors("AllowAnyOrigin"); 
            app.UseSwagger();
            app.UseAuthorization();
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
