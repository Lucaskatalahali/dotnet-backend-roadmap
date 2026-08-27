using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.Endpoints;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

// 1. Lê a chave que você definiu no appsettings.json
var connectionSString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registra o AppDbContext configurando-o para usar PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionSString));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapCategoryEndpoints();
app.MapBookEndpoints();

app.Run();
