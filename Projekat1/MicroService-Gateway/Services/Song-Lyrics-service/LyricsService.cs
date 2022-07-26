using MicroService_Gateway.Services.Urls;
using MicroService_Gateway.DTO;
using RestSharp;

namespace MicroService_Gateway.Services
{
    public class LyricsService : ILyricsService
    {
        private readonly RestClient _client;
        public LyricsService() { 
            _client = new RestClient(ServiceUrls.LYRICS_URL);
        }
        public async Task<LyricsForSong?> GetLyricsAsync(string? artist, string? track)
        {
            try
            {
                if (artist == null || track == null) return null;
                var request = new RestRequest($"{artist}/{track}");
                var response = await _client.ExecuteGetAsync<LyricsForSong>(request);
                if (!response.IsSuccessful)
                {
                    return null;   
                }
                return response.Data;
                
            }
            catch (Exception ex)
            {    
                throw ex;
            }
        }

       
    }
}