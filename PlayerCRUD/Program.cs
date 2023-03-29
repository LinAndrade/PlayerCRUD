using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
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

        app.MapPost("/player", (Player players) =>
        {
            PlayerRepository.Add(players);
            return Results.Created($"/players/{players.Name}", players.Name);
        });

        app.MapGet("/player/{name}", ([FromRoute] string name) =>
        {
            var players = PlayerRepository.Getby(name);
            if (players != null)
            {
                return Results.Ok(players);
            }
            else
            {
                return Results.NotFound();
            }
        });

        app.MapPut("/player", (Player players) =>
        {
            var playersSaved = PlayerRepository.Getby(players.Name);
            playersSaved.Posicao = players.Posicao;
            return Results.Ok();
        });

        app.MapDelete("/player/{name}", ([FromRoute] string name) =>
        {
            var playersSaved = PlayerRepository.Getby(name);
            PlayerRepository.Remove(playersSaved);
            return Results.NoContent();
        });

        app.Run();
    }
}
