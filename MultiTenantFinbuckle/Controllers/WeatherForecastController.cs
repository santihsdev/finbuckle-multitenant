using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MulltiTenantFinbuckle.Models;
using static System.DateOnly;
using static System.DateTime;

namespace MulltiTenantFinbuckle.Controllers;

[ApiController]
[Route("/sus/sus")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] SusSummaries =
    {
        "Sus", "Sus", "Sus"
    };

    private static readonly string[] VaultSummaries =
    {
        "Vault", "Vault", "Vault"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var value = HttpContext.GetMultiTenantContext<MultiTenantInfo>()?.TenantInfo;
        if (value?.Identifier == "sus")
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = FromDateTime(Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = SusSummaries[Random.Shared.Next(SusSummaries.Length)]
                })
                .ToArray();
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = FromDateTime(Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = VaultSummaries[Random.Shared.Next(VaultSummaries.Length)]
            })
            .ToArray();
    }
}