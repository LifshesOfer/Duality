using Duality;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();




app.MapPost("/wordscore", ([FromBody] WordScoreConfiguration config) => Results.Ok(CalculateWordScore(config)));

app.Run();
int CalculateWordScore(WordScoreConfiguration scoreConfiguration)
{
    var count = 0;
    var links = GetAllLinks(scoreConfiguration.Url);
    foreach(var link in links)
    {
        var page = BuiltInFunctions.DownloadPage(link);
        count += BuiltInFunctions.CountWordOccurences(page, scoreConfiguration.Word);
    }
    return count;
}

HashSet<string> GetAllLinks(string url)
{
    var allLinks = BuiltInFunctions.GetLinksInPage(url);
    foreach(var link in allLinks)
    {
        allLinks.UnionWith(BuiltInFunctions.GetLinksInPage(link));
    }
    return allLinks;
}

internal record WordScoreConfiguration(string Url, string Word);

