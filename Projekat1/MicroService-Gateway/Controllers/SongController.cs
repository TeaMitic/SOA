// using MicroService_Gateway.CSV;
using Microsoft.AspNetCore.Mvc;
using MicroService_Gateway.Services;
using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;

namespace MicroService_Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class SongController : ControllerBase
{
   
    private readonly ISongService _songService;
    private readonly ILyricsService _lyricsService;

    public SongController(ISongService songService, ILyricsService lyricsService)
    {
        _songService = songService;
        _lyricsService = lyricsService;
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
            Song? song = await _songService.GetOneAsync(artist,track);
            if (song == null) 
            { 
                return StatusCode(400,"Error: Song not found."); //refactor: message is sent from other microservice
            }
            string? lyrics = await _lyricsService.GetLyricsAsync(artist,track);

            SongAndLyrics songAndLyrics = new SongAndLyrics(song,lyrics);                
            return StatusCode(200,songAndLyrics);    
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);    
        }
    }


}
