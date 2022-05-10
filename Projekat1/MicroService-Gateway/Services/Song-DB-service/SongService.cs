using MicroService_Gateway.Models;
using MicroService_Gateway.Services.Urls;
using MicroService_Gateway.DTO;
using RestSharp;


namespace MicroService_Gateway.Services
{
    public class SongService : ISongService
    {
        private readonly RestClient _client;
        public SongService() { 
            _client = new RestClient(ApiUrls.DB_URL);
        }
        public Task AddOneAsync(Song song)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(string artist, string track)
        {
            throw new NotImplementedException();
        }

        public Task EditOneAsync(string artist, string track, Song updateObject)
        {
            throw new NotImplementedException();
        }

        public async Task<Song> GetOneAsync(string artist, string track)
        {
            var request = new RestRequest($"get/{artist}/{track}");
            var response = await _client.ExecuteGetAsync<Song>(request);
            Console.WriteLine(response.Content);
            if (!response.IsSuccessful)
            {
                return null;   
            }
            return response.Data;
        }
    }
}