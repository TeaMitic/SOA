// using MicroService_Gateway.CSV;
using Microsoft.AspNetCore.Mvc;
using MicroService_Gateway.Services;
using MicroService_Gateway.Models;

namespace MicroService_Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
   
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    // [HttpPost(Name = "LoadDB")]
    // public async Task<IActionResult> LoadDB()
    // {
    //     var data = async 
    // }

    [HttpGet]
    [Route("GetOne/{artist}/{track}")]
    public async Task<IActionResult> GetOne([FromRoute]string artist, [FromRoute] string track)
    { 
        try
        {
            Song song = await _songService.GetOneAsync(artist,track);
            if (song != null) 
            { 
                return StatusCode(200,song);    
            }
            return StatusCode(400,"Song not found."); //refactor: message is sent from other microservice
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);    
        }
    }


}
