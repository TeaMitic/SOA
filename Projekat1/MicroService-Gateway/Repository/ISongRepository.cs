using MicroService_Gateway.Models; 

namespace MicroService_Gateway.Repository 
{ 
    public interface ISongRepository 
    {
        Task<Song> GetOne(string artist, string track);
        Task EditOne(string artist, string track, Song updateObject);
        Task DeleteOne(string artist, string track);
        Task AddOne(Song song);
        //Task ImportFromCS(nesto)


    }

}