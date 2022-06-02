using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;
using MicroService_Gateway.Services.Urls;
using RestSharp;
using Newtonsoft.Json;
using MicroService_Gateway.CSVWrapper;

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
                    if (response.ResponseStatus == ResponseStatus.None) 
                    {
                        throw response.ErrorException;
                    }
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
                    if (response.ResponseStatus == ResponseStatus.None) 
                    {
                        throw response.ErrorException;
                    }
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
                    if (response.ResponseStatus == ResponseStatus.None) 
                    {
                        throw response.ErrorException;
                    }
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
                var response = await _client.ExecuteGetAsync<Song>(request); //need refactoring 
                if (!response.IsSuccessful)
                {
                    Console.WriteLine(response.ToString());
                    if (response.ResponseStatus == ResponseStatus.None) 
                    {
                        throw response.ErrorException;
                    }
                    return null;   
                }
                return response.Data;
                
            }
            catch (Exception ex)
            {    
                throw ex;
            }
        }

       

        public async Task<bool> LoadFromCSV(string filename,int chunkSize)
        {
            try
            {
                CsvHelperWrapper csvWrapper = new CsvHelperWrapper(filename);
                int numOfsongsLoaded = 0;
                int songsLeft = -1;
                bool successfull = false;
                bool eof = false;
                IList<Song> songsLoadedInChunk = new List<Song>();
                while (!eof) 
                { 
                    songsLoadedInChunk = csvWrapper.ReadMoreRecords(numOfsongsLoaded,chunkSize, ref eof);
                    songsLeft = songsLoadedInChunk.Count();
                    numOfsongsLoaded += songsLeft;
                    //db service call 
                    var request = new RestRequest($"addMany").AddJsonBody(songsLoadedInChunk);
                    var response = await _client.ExecutePostAsync(request); 
                    if (!response.IsSuccessful)
                    {
                        if (response.ResponseStatus == ResponseStatus.None) 
                        {
                            throw response.ErrorException;
                        }
                        successfull = false;
                        break;
                    }
                    successfull = true;
                }
                return successfull;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}