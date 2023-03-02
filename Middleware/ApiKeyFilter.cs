using System.Net;
using Microsoft.Extensions.Options;

public class ApiKeyFilter
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public ApiKeyFilter(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        //check if the request is preflight request
        if (context.Request.Method == "OPTIONS")
        {
            await _next.Invoke(context);
        }

        var apiKey = context.Request.Headers["X-API-KEY"];
        List<string>? accessSecrets = _configuration.GetSection("AccessSecrets").Get<List<string>>();

        var isInwhiteListIPList = accessSecrets != null && accessSecrets.Any(secret => secret == apiKey);

        if (!isInwhiteListIPList)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }
        await _next.Invoke(context);
    }
}