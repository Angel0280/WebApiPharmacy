using WebApi.Business.Interfaces;
using WebApi.Business.Services;
using WebAPi.DataAccess.Intarfaces;
using WebAPi.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro de services y repository del proyecto para asegurar la inyeccion de dependencias
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
