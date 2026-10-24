using ApiUFC.Data;
using ApiUFC.Interfaces;
using ApiUFC.Models;
using ApiUFC.Services;
using Microsoft.EntityFrameworkCore;

namespace ApiUFC.Minimal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext registreren (zelfde connection string naam)
            builder.Services.AddDbContext<UfcDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Zelfde DI-switch als in je eerste project
            bool useDatabase = true; // zet op false om in-memory services te gebruiken

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

            // Alleen Fighters-service voorlopig
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //
            //  FIGHTERS MINIMAL ENDPOINTS
            //
            var fightersGroup = app.MapGroup("/api/fighters");

            // GET: alle fighters
            fightersGroup.MapGet("/", async (IFightersService service) =>
            {
                var fighters = await service.GetFighters();
                return Results.Ok(fighters);
            });

            // GET: fighter by id
            fightersGroup.MapGet("/{id:int}", async (int id, IFightersService service) =>
            {
                var fighter = await service.GetFighter(id);
                return fighter is null ? Results.NotFound() : Results.Ok(fighter);
            });

            // POST: nieuwe fighter
            fightersGroup.MapPost("/", async (Fighter fighter, IFightersService service) =>
            {
                await service.CreateFighter(fighter);
                return Results.Created($"/api/fighters/{fighter.Id}", fighter);
            });

            // PUT: update fighter
            fightersGroup.MapPut("/{id:int}", async (int id, Fighter fighter, IFightersService service) =>
            {
                var updated = await service.UpdateFighter(id, fighter);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            });

            // DELETE: fighter verwijderen
            fightersGroup.MapDelete("/{id:int}", async (int id, IFightersService service) =>
            {
                var existing = await service.GetFighter(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteFighter(id);
                return Results.NoContent();
            });

            var divisionsGroup = app.MapGroup("/api/divisions");

            // GET: alle divisions
            divisionsGroup.MapGet("/", async (IDivisionsService service) =>
            {
                var divisions = await service.GetDivisions();
                return Results.Ok(divisions);
            });

            // GET: division by id
            divisionsGroup.MapGet("/{id:int}", async (int id, IDivisionsService service) =>
            {
                var division = await service.GetDivision(id);
                return division is null ? Results.NotFound() : Results.Ok(division);
            });

            // POST: nieuwe division
            divisionsGroup.MapPost("/", async (Division division, IDivisionsService service) =>
            {
                await service.CreateDivision(division);
                return Results.Created($"/api/divisions/{division.Id}", division);
            });

            // PUT: division updaten
            divisionsGroup.MapPut("/{id:int}", async (int id, Division division, IDivisionsService service) =>
            {
                var updated = await service.UpdateDivision(id, division);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            });

            // DELETE: division verwijderen
            divisionsGroup.MapDelete("/{id:int}", async (int id, IDivisionsService service) =>
            {
                var existing = await service.GetDivision(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteDivision(id);
                return Results.NoContent();
            });

            var nationsGroup = app.MapGroup("/api/nations");

            // GET: alle nations
            nationsGroup.MapGet("/", async (INationsService service) =>
            {
                var nations = await service.GetNations();
                return Results.Ok(nations);
            });

            // GET: nation by id
            nationsGroup.MapGet("/{id:int}", async (int id, INationsService service) =>
            {
                var nation = await service.GetNation(id);
                return nation is null ? Results.NotFound() : Results.Ok(nation);
            });

            // POST: nieuwe nation
            nationsGroup.MapPost("/", async (Nation nation, INationsService service) =>
            {
                await service.CreateNation(nation);
                return Results.Created($"/api/nations/{nation.Id}", nation);
            });

            // PUT: nation updaten
            nationsGroup.MapPut("/{id:int}", async (int id, Nation nation, INationsService service) =>
            {
                var updated = await service.UpdateNation(id, nation);
                return updated is null ? Results.NotFound() : Results.Ok(updated);
            });

            // DELETE: nation verwijderen
            nationsGroup.MapDelete("/{id:int}", async (int id, INationsService service) =>
            {
                var existing = await service.GetNation(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteNation(id);
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
