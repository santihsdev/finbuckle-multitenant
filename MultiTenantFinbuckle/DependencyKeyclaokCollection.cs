using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authentication.Claims;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MulltiTenantFinbuckle;

public static class DependencyKeyclaokCollection
{
    public static AuthenticationBuilder AddKeycloakAuthenticationCustom(this IServiceCollection services,
        Action<KeycloakAuthenticationOptions?> action)
    {
        var keycloakOptions = new KeycloakAuthenticationOptions
        {
            Realm = "sus",
            AuthServerUrl = "https://login.rohmer.jala-one.com/",
            Resource = "client-sus",
            Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = "rRrGMS3K5S5cZd17ozDy5AOjxgwr5dNw"
            },
            VerifyTokenAudience = true,
        };
        return services.AddKeycloakAuthentication(keycloakOptions,
            opt => opt.Audience = "account");
    }

    public static AuthenticationBuilder AddKeycloakAuthenticationCustom(this IServiceCollection services)
    {
        return services.AddKeycloakAuthenticationCustom(_ => { });
    }
}