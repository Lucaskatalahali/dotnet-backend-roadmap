using FluentValidation;
using Library_API.Data;
using Library_API.Endpoints;
using Library_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<LoanService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapBookEndpoints();
app.MapMemberEndpoints();
app.MapLoanEndpoints();

app.Run();
