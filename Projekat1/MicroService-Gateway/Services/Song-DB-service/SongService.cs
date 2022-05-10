using MicroService_Gateway.Models;
using MicroService_Gateway.Services.Urls;
using RestSharp;


namespace MicroService_Gateway.Services
{
    public class SongService : ISongService
    {
        private readonly RestClient _client;
        public SongService() { 
            _client = new RestClient(ApiUrls.DB_URL);
        }
        public Task<bool> AddOneAsync(Song song)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteOneAsync(string artist, string track)
        {
            // throw new NotImplementedException();
            try
            {
                var request = new RestRequest($"delete/{artist}/{track}");
                var response = await _client.DeleteAsync(request);
                if (!response.IsSuccessful)
                {
                    // Console.WriteLine(response.Content); 
                    return false;   
                }
                return true;               
            }
            catch (Exception ex)
            {    
                throw ex;
            }

        }

        public Task<bool> EditOneAsync(string artist, string track, Song updateObject)
        {
            throw new NotImplementedException();
        }

        public async Task<Song?> GetOneAsync(string artist, string track)
        {
            try
            {
                var request = new RestRequest($"get/{artist}/{track}");
                var response = await _client.ExecuteGetAsync<Song>(request);
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.Content); 
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