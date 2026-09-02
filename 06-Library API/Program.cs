using System.Text;
using FluentValidation;
using Library_API.Data;
using Library_API.Endpoints;
using Library_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

/*builder.Services.AddAuthentication()
    .AddJwtBearer();*/

builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]!
                )
            ),

            ValidateAudience = false,
            ValidateIssuer = false 
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<LoanService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();
app.MapBookEndpoints();
app.MapMemberEndpoints();
app.MapLoanEndpoints();

app.Run();
