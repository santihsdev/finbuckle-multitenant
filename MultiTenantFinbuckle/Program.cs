using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MulltiTenantFinbuckle.Db;
using MulltiTenantFinbuckle.Models;
using MulltiTenantFinbuckle.Tenant.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TenantAdminDbContext>(opt
    => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddMultiTenant<MultiTenantInfo>()
    .WithStrategy<TokenStrategy>(ServiceLifetime.Transient)
    .WithEFCoreStore<TenantAdminDbContext, MultiTenantInfo>()
    .WithPerTenantAuthentication()
    .WithPerTenantOptions<JwtBearerOptions>((opt, tenantInfo) =>
    {
        opt.Authority = tenantInfo.AuthServerUrl;
        opt.Audience = tenantInfo.Resource;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = "account",
            ValidIssuer = tenantInfo.AuthServerUrl,
            NameClaimType = "preferred_username",
            RoleClaimType = "role"
        };
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