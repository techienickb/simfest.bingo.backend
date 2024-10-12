using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Metrics;
using Simfest.Bingo.Backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR().AddAzureSignalR(builder.Configuration["AZURE_SIGNALR_CONNECTIONSTRING"]);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "bingo",
                  builder =>
                  {
                      builder.WithOrigins("https://localhost:3000",
                                          "http://localhost:3000",
                                          "https://bingo.simfest.co.uk")
                      .AllowAnyHeader().AllowCredentials().AllowAnyMethod();
                  });

});
builder.Services.AddOpenTelemetry().UseAzureMonitor();

var app = builder.Build();

app.UseCors("bingo");

app.UseHttpsRedirection();
app.UseRouting();
app.MapHub<BingoHub>("/hub");

app.Run();