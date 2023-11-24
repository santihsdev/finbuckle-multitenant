using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MulltiTenantFinbuckle.Controllers;

[ApiController]
[Route("/cns/[controller]")]
public class CnsController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public string Get()
    {
        return "CNS";
    }
}