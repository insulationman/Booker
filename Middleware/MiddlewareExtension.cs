public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseIPFilter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<IPFilter>();
    }
}