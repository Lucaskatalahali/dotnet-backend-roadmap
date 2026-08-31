public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Não queremos que o método GET seja atingido por esse middleware.
        if(context.Request.Method == "POST" &&
        context.Request.Path.StartsWithSegments("/books"))
        {
            const string validApiKey = "admin123";

            //X-API-Key é apenas um nome que nós escolhemos para transportar a chave na request.
            var apiKey = context.Request.Headers["X-API-Key"].FirstOrDefault();

            if(validApiKey != apiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or missing API key.");

                return;
            }
        }

        await _next(context);
    }
}