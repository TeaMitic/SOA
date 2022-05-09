using MicroService_Gateway.CSV;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class GatewayController : ControllerBase
{
   

    private readonly ILogger<GatewayController> _logger;

    public GatewayController(ILogger<GatewayController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "LoadDB")]
    public async Task<IActionResult> LoadDB()
    {
        var data = async 
    }


}
