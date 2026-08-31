public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        Console.WriteLine(
            $"{context.Request.Method} " +
            $"{context.Request.Path} " +
            $"{context.Response.StatusCode}");
    }
}