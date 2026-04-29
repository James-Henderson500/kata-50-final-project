using Microsoft.EntityFrameworkCore;
using VisaApplicationAPI.Models;
using VisaApplicationAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder. Configuration.GetConnectionString("Server=localhost;Database=VisaAppDB;User Id=sa;Password=DevPassword1;TrustServerCertificate=True;")));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();