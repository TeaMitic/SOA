using MicroService_Gateway.Services.Urls;
using RestSharp;

namespace MicroService_Gateway.Services
{
    public class LyricsService : ILyricsService
    {
        private readonly RestClient _client;
        public LyricsService() { 
            _client = new RestClient(ApiUrls.LYRICS_URL);
        }
        public async Task<string?> GetLyricsAsync(string artist, string track)
        {
            try
            {
                var request = new RestRequest($"{artist}/{track}");
                var response = await _client.ExecuteGetAsync<string>(request);
                if (!response.IsSuccessful)
                {
                    return response.Data;   
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