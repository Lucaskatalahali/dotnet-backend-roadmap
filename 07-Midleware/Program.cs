var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.MapGet("/", () => "App is running...");

app.MapPost("/books", () =>
{
    return Results.Ok("Book created");
});

app.Run();
