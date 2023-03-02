public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseApiKeyFilter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyFilter>();
    }
}