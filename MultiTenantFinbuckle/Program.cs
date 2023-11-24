using Finbuckle.MultiTenant;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MulltiTenantFinbuckle.Tenant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddMultiTenant<TenantInfo>()
    .WithStrategy<PathStrategy>(ServiceLifetime.Transient)
    .WithInMemoryStore(opt =>
    {
        opt.IsCaseSensitive = true;
        opt.Tenants.Add(new TenantInfo
        {
            Id = "tenant1",
            Identifier = "sus",
            Name = "Sussex",
        });
        opt.Tenants.Add(new TenantInfo
        {
            Id = "tenant2",
            Identifier = "cns",
            Name = "CNS",
        });
    })
    .WithPerTenantAuthentication()
    .WithPerTenantOptions<JwtBearerOptions>((opt, tenantInfo) =>
    {
        if (tenantInfo.Identifier == "sus")
        {
            opt.Authority = "https://login.rohmer.jala-one.com/realms/sus";
            opt.Audience = "client-sus";
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = "account",
                ValidIssuer = "https://login.rohmer.jala-one.com/realms/sus",
                NameClaimType = "preferred_username",
                RoleClaimType = "role"
            };
        }
        else
        {
            opt.Authority = "https://login.rohmer.jala-one.com/realms/vault";
            opt.Audience = "admin-vault";
            opt.SaveToken = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = "account",
                ValidIssuer = "https://login.rohmer.jala-one.com/realms/vault",
                NameClaimType = "preferred_username",
                RoleClaimType = "role"
            };
        }
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMultiTenant();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();