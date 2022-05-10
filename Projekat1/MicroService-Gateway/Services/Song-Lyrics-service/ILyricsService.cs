namespace MicroService_Gateway.Services 
{
    public interface ILyricsService 
    { 
        Task<string?> GetLyricsAsync(string artist, string track); 
    }
}