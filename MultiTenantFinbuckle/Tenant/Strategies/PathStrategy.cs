using Finbuckle.MultiTenant;

namespace MulltiTenantFinbuckle.Tenant.Strategies;

public class PathStrategy : IMultiTenantStrategy
{
    public Task<string?> GetIdentifierAsync(object context)
    {
        if (context is not HttpContext httpContext) return Task.FromResult<string?>(null);
        var path = httpContext.Request.Path;
        var segments = path.Value?.Split('/');
        Console.WriteLine($"Path: {segments?[1]}");
        return ((segments?.Length > 1)
            ? Task.FromResult(segments[1])
            : Task.FromResult(""))!;
    }
}