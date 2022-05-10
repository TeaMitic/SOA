using MicroService_Gateway.Models; 

namespace MicroService_Gateway.Services 
{ 
    public interface ISongService 
    {
        Task<Song?> GetOneAsync(string artist, string track);
        Task<bool> EditOneAsync(string artist, string track, Song updateObject);
        Task<bool> DeleteOneAsync(string artist, string track);
        Task<bool> AddOneAsync(Song song);
        //Task ImportFromCS(nesto)


    }

}