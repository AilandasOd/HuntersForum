using HuntersForum.Data;
using Microsoft.EntityFrameworkCore;
using System;
var builder = WebApplication.CreateBuilder(args);

// Pridedame paslaugas prie DI konteinerio
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Pridedame ForumContext su SQL Server konfigūracija
var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddCors();
builder.Services.AddDbContext<ForumContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Swagger dokumentacijai (API dokumentavimui)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Plėtojimo aplinkos patikrinimas, ar reikia naudoti Swagger (tik developmente)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ForumContext>();

    await SeedData.Seed(context); // Iškviečiame Seed metodą
}

// Maršrutų ir vidurinės programinės įrangos konfigūracija
app.UseAuthorization();

app.MapControllers();

app.Run();
