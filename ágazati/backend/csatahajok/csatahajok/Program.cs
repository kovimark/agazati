
using System.Text.Json.Serialization;

namespace csatahajok
{
    public class Program
    {

        public static string UID = "abcd"; // Itt kell a Deletehez szükséges kódot megadni.

        /*
          https://localhost:7074/api/Csata/Resztvevok/{name}
         
         A feladatban ilyen végpontok lesznek. 

        https://localhost:7074/api/ -> automatikusan létrehozza a Controller, nem kell vele foglalkozni.
         
         Csata -> A Controller neve. Ha a Controllert így nevezed el: CsataController, tudni fogja, hogy csak a Csata-t kell leszednie.

        Resztvevok -> A végpont neve. A függvénnyel tudod megadni.

        {name} -> url-ben megadott változónév

         
         */




        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
            


            var app = builder.Build(); // builder-rel kezõdik e sor felé, app-pal kezdõdik e sor alá.

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
