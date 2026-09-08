using FirstAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = 
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }); // OPTIUNE DE IGNORARE A CICLURILOR
    
// CORS - Cross-Origin Resource Sharing
// Origine = Protocol + Domeniu + Port | In cazu nostru, avem cross-origin pt ca port-urile de backend si frontend difera
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin() // Permite oricarui site
              .AllowAnyMethod() // Permite GET,POST,PUT,DELETE,ETC
              .AllowAnyHeader(); // Permite JSON, etc.
    });
});
var app = builder.Build();

app.UseCors("AllowBlazor"); // ii zicem sa permita cors pt blazor frontend

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Servire fisiere statice din wwwroot folder
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
