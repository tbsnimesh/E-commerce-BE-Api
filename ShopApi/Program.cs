using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ShopApi.Api.Endpoints;
using ShopApi.Data;
using ShopApi.Data.Seed;
using ShopApi.Services;
using ShopApi.Services.Abstractions;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           //This warning is intentionally off � see PROJECT_NOTES.md
           .ConfigureWarnings(w => w.Ignore(
               Microsoft.EntityFrameworkCore.Diagnostics.CoreEventId
                   .PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning)));

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("ShopApi")
               .WithTheme(ScalarTheme.BluePlanet)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

app.UseHttpsRedirection();

app.MapProductEndpoints();

app.Run();
 
