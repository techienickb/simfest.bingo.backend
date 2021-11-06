using Simfest.Bingo.Backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("bingo");

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<BingoHub>("/hub");
});

app.Run();