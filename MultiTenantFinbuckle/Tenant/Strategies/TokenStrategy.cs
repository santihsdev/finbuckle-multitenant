using System.IdentityModel.Tokens.Jwt;
using Finbuckle.MultiTenant;
using Microsoft.IdentityModel.Tokens;

namespace MulltiTenantFinbuckle.Tenant.Strategies;

public class TokenStrategy : IMultiTenantStrategy
{
    public Task<string?> GetIdentifierAsync(object context)
    {
        if (context is not HttpContext httpContext) return Task.FromResult<string?>(null);
        var identity = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (identity is null) return Task.FromResult<string?>(null!);
        
        var token = identity.Split(" ")[1];
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(token);
        var claims = securityToken.Claims;
        
        var iss = claims.FirstOrDefault(c => c.Type == "iss");
        if (iss == null || string.IsNullOrEmpty(iss.Value))
        {
            throw new SecurityTokenValidationException("Error in exception");
        }
        
        var tenant = ParseRealm(iss.Value);
        return Task.FromResult(tenant)!;
    }

    private static string ParseRealm(string realm)
    {
        var parts = realm.Split("/");
        if (parts.Length > 0)
        {
            return parts[parts.Length - 1];
        }

        throw new SecurityTokenValidationException("Error in exception");
    }
}