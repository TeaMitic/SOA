// using MicroService_Gateway.CSV;
using Microsoft.AspNetCore.Mvc;
using MicroService_Gateway.Repository;
using MicroService_Gateway.Models;

namespace MicroService_Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class GatewayController : ControllerBase
{
   
    private readonly ISongRepository _songRepository;
    private readonly ILogger<GatewayController> _logger;

    public GatewayController(ISongRepository songRepository, ILogger<GatewayController> logger)
    {
        _songRepository = songRepository;
        _logger = logger;
    }

    // [HttpPost(Name = "LoadDB")]
    // public async Task<IActionResult> LoadDB()
    // {
    //     var data = async 
    // }

    [HttpGet(Name = "Get/{artist}/{track}")]
    public async Task<IActionResult> GetOne(string artist, string track)
    { 
        try
        {
            Song song = await _songRepository.GetOne(artist,track);
            // if (song)
            return StatusCode(200,{});


        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);    
        }
    }


}
