using System.Net;
using Microsoft.Extensions.Options;

public class IPFilter
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    public IPFilter(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress;
        List<string> whiteListIPList = _configuration.GetSection("IPWhitelist").Get<List<string>>();

        var isInwhiteListIPList = whiteListIPList.Any(ip => ip == ipAddress.ToString());
        if (!isInwhiteListIPList)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }
        await _next.Invoke(context);
    }
}