using MicroService_Gateway.Models; 

namespace MicroService_Gateway.Services 
{ 
    public interface ISongService 
    {
        Task<Song?> GetOneAsync(string artist, string track);
        Task EditOneAsync(string artist, string track, Song updateObject);
        Task DeleteOneAsync(string artist, string track);
        Task AddOneAsync(Song song);
        //Task ImportFromCS(nesto)


    }

}