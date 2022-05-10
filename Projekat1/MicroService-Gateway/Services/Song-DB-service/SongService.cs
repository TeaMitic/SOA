using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;
using MicroService_Gateway.Services.Urls;
using RestSharp;
using Newtonsoft.Json;


namespace MicroService_Gateway.Services
{
    public class SongService : ISongService
    {
        private readonly RestClient _client;
        public SongService() { 
            _client = new RestClient(ApiUrls.DB_URL);
        }
        public async Task<bool> AddOneAsync(Song song)
        {
            try
            {
                var request = new RestRequest($"addOne").AddBody(song);
                var response = await _client.ExecutePostAsync(request);
                if (!response.IsSuccessful)
                {
                    return false;   
                }
                return true;               
            }
            catch (Exception ex)
            {    
                throw ex;
            }
        }

        public async Task<bool> DeleteOneAsync(string artist, string track)
        {
            try
            {
                var request = new RestRequest($"delete/{artist}/{track}");
                var response = await _client.DeleteAsync(request);
                if (!response.IsSuccessful)
                {
                    return false;   
                }
                return true;               
            }
            catch (Exception ex)
            {    
                throw ex;
            }

        }

        public async Task<bool> EditOneAsync(EditSong updatedSong)
        {
            // throw new NotImplementedException();
            try
            {
                var request = new RestRequest($"editOne").AddBody(updatedSong);
                Console.WriteLine(JsonConvert.SerializeObject(updatedSong));
                var response = await _client.ExecutePutAsync(request);
                if (!response.IsSuccessful)
                {
                    return false;   
                }
                return true;
                
            }
            catch (Exception ex)
            {    
                throw ex;
            }
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