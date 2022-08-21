using Microsoft.AspNetCore.Mvc;
using MicroService_Gateway.Services;
using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;
using Newtonsoft.Json;
using MicroService_Gateway.CSVWrapper;
using MicroService_Gateway.MQTT;
using System.Text.Json;

namespace MicroService_Gateway.Controllers;

[ApiController]
[Route("api/songs")]
public class SongController : ControllerBase
{
   
    private readonly ISongService _songService;
    private readonly ILyricsService _lyricsService;

    public SongController(ISongService songService, ILyricsService lyricsService)
    {
        _songService = songService;
        _lyricsService = lyricsService;
    }



    /// <summary> 
    /// Loads songs in database from csv file.
    /// </summary>
    /// <returns></returns>
    /// <param name="chunkSize">Chunk size for songs (How much songs to read from csv at once).</param>
    /// <response code="200">Informs that songs are correctly loaded in db.</response>
    /// <response code="400">Informs that client error occured during loading. (chunkSize less than 1)</response>
    /// <response code="500">Informs that server error occured during loading.</response>
    [HttpPost]
    [Route("LoadDBfromCSV/{chunkSize}")]
    public async Task<IActionResult> LoadDBFromCSV([FromRoute] int chunkSize = 10) 
    {
        try
        {
            if (chunkSize <= 0) 
            {
                return StatusCode(400, "Error: Chunk size must be positive number");

            }
            // bool result = await _songService.LoadFromCSV("..\\SongsDataset\\top50.csv",chunkSize); //non docker 
            bool result = await _songService.LoadFromCSV("/home/datasets/Songs/top50.csv",chunkSize); //docker 
            if (result) 
            {
                return StatusCode(200,"Loaded into db.");
            }
            return StatusCode(400, "Error: Failed to load db from CSV file");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500,ex.Message);            
        }
    }

    /// <summary>
    /// Gets songs from database lyrics. 
    /// </summary>
    /// <returns></returns>
    /// <param name="limit">Limit on number of songs being returned.</param>
    /// <response code="200">Returns song with or without lyrics.</response>
    /// <response code="400">Some parameter is null or song does not exist in database.</response>
    /// <response code="500">Informs that server error occured during getting the song.</response>
    [HttpGet]
    [Route("GetSongs/{limit}")]
    public async Task<IActionResult> GetSongs([FromRoute] int limit=5) 
    {
        try
        {
            IList<Song?> songs = await _songService.GetSongs(limit);
            if (songs == null) 
            { 
                return StatusCode(400,"Error: Songs not found."); //refactor: message is sent from other microservice
            }
            IList<SongAndLyrics> songsWithLyrics = new List<SongAndLyrics>();
            foreach (Song? song in songs)
            {
                if (song == null) continue;
                LyricsForSong? lyrics = await _lyricsService.GetLyricsAsync(song.ArtistName,song.TrackName);
                SongAndLyrics? songLyr = new SongAndLyrics(song,lyrics);
                songsWithLyrics.Add(songLyr);
            }

            return StatusCode(200,songsWithLyrics);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);    
        }
    } 
    
    /// <summary> 
    /// Gets one song.
    /// </summary>
    /// <returns>Returns song with lyrics if exists.</returns>
    /// <param name="artist">Artist's full name</param>
    /// <param name="track">Track's full name</param>
    /// <response code="200">Returns song with or without lyrics.</response>
    /// <response code="400">Some parameter is null or song does not exist in database.</response>
    /// <response code="500">Informs that server error occured during getting the song.</response>

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
            LyricsForSong? lyrics = await _lyricsService.GetLyricsAsync(artist,track);

            SongAndLyrics songAndLyrics = new SongAndLyrics(song,lyrics);    
            return StatusCode(200,songAndLyrics);    
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);    
        }
    }
    
    /// <summary>
    /// Deletes a song.
    /// </summary>
    /// <param name="artist">Artist's full name</param>
    /// <param name="track">Track's full name</param>
    /// <response code="200">Informs that song is deleted.</response>
    /// <response code="400">Some parameter is null or song does not exist in database.</response>
    /// <response code="500">Informs that server error occured during deleting the song.</response>
    [HttpDelete]
    [Route("DeleteOne/{artist}/{track}")]
    public async Task<IActionResult> DeleteOne([FromRoute]string artist,[FromRoute]string track)
    {
        try
        {
           bool response = await _songService.DeleteOneAsync(artist,track);
           if(!response)
           {
                return StatusCode(400, "Song not found.");
           } 
           return StatusCode(200, "Song successfully deleted.");
        }
        catch (Exception ex)
        {            
            return StatusCode(500,ex.Message);    
        }
    }

    /// <summary>
    /// Adds one song in db.
    /// </summary>
    /// <remarks>
    /// Sample requrest: 
    /// 
    ///     {
    ///         "trackName": "Lose yourself",
    ///         "artistName": "Eminem",
    ///         "genre": "rap",
    ///         "beatsPerMinute": 25,
    ///         "energy": 30,
    ///         "danceability": 15,
    ///         "loudnessIndB": 40,
    ///         "liveness": 10,
    ///         "valence": 50,
    ///         "length": 60,
    ///         "acousticness": 10,
    ///         "speechiness": 80,
    ///         "popularity": 100
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Informs that song is added.</response>
    /// <response code="400">Some parameter is null or song already exists.</response>
    /// <response code="500">Informs that server error occured during adding the song.</response>
    [HttpPost]
    [Route("AddOne")]
    public async Task<IActionResult> AddOne([FromBody]Song song)
    {
        try
        {
            bool response = await _songService.AddOneAsync(song);
            if(!response)
            {
                return StatusCode(400, "Song not added.");
            }
            return StatusCode(200, "Song successfully added");            
        }
        catch (Exception ex)
        {            
            return StatusCode(500,ex.Message);    
        }
    }


    
    /// <summary>
    /// Edits one song in db.
    /// </summary>
    /// <remarks>
    /// Sample requrest: 
    /// 
    ///     {
    ///         "trackName": "Lose yourself",
    ///         "artistName": "Eminem",
    ///         "genre": "rap",
    ///         "beatsPerMinute": 25,
    ///         "energy": 30,
    ///         "danceability": 15,
    ///         "loudnessIndB": 40,
    ///         "liveness": 10,
    ///         "valence": 50,
    ///         "length": 60,
    ///         "acousticness": 10,
    ///         "speechiness": 80,
    ///         "popularity": 100
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Informs that song is edited.</response>
    /// <response code="400">Some parameter is null or song does exist in database.</response>
    /// <response code="500">Informs that server error occured during editing the song.</response>
    [HttpPut]
    [Route("EditOne")]
    public async Task<IActionResult> EditOne([FromBody]EditSong updatedSong)
    {
        try
        {
            bool response = await _songService.EditOneAsync(updatedSong);
            if(!response)
            {
                return StatusCode(400, "Song not updated.");
            }
            return StatusCode(200, "Song successfully updated");      
            
        }
        catch (Exception ex)
        {            
            return StatusCode(500,ex.Message);    
        }
    }


}
