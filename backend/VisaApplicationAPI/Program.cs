using Microsoft.EntityFrameworkCore;
using VisaApplicationAPI.Models;
using VisaApplicationAPI.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder. Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{    
    app.UseSwagger();    
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();