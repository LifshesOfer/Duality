using DualityApplication.ExternalFunctions;
using DualityApplication.Interfaces;
using DualityApplication.Models;
using DualityApplication.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IExternalFunctions>(new ExternalFunctions());
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/wordscore", ([FromBody] WordScoreConfiguration config, [FromServices]IExternalFunctions extFunctions, int? timeout) =>
{
    timeout ??= -1;
    try
    {
        var countService = new WordCountService(extFunctions);

        var countTask = Task.Run(() => countService.CalculateWordScore(config));
        var result = countTask.Wait(timeout.Value) ? Results.Ok(countTask.Result) : Results.Problem($"Timeout exceeded after {timeout}ms", statusCode: StatusCodes.Status408RequestTimeout);

        return result;
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.Run();


