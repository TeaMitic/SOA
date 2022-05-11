using MicroService_Gateway.DTO;

namespace MicroService_Gateway.Services 
{
    public interface ILyricsService 
    { 
        Task<LyricsForSong?> GetLyricsAsync(string artist, string track); 
    }
}