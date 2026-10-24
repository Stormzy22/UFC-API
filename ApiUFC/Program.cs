using Microsoft.EntityFrameworkCore;
using ApiUFC.Data;
using ApiUFC.Interfaces;
using ApiUFC.Services;
using ApiUFC.Services;

namespace ApiUFC
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<UfcDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // HIER kies je: database of in-memory
            bool useDatabase = false; // zet op false voor in-memory

            if (useDatabase)
            {
                builder.Services.AddScoped<IFightersService, DbFightersService>();
                builder.Services.AddScoped<IDivisionsService, DbDivisionsService>();
                builder.Services.AddScoped<INationsService, DbNationsService>();
            }
            else
            {
                builder.Services.AddSingleton<IFightersService, FightersService>();
                builder.Services.AddSingleton<IDivisionsService, DivisionsService>();
                builder.Services.AddSingleton<INationsService, NationsService>();
            }

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            var app = builder.Build();

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
