using MicroService_Gateway.Models; 
using MicroService_Gateway.DTO;

namespace MicroService_Gateway.Services 
{ 
    public interface ISongService 
    {
        Task<Song?> GetOneAsync(string artist, string track);
        Task<bool> EditOneAsync(EditSong updateSong);
        Task<bool> DeleteOneAsync(string artist, string track);
        Task<bool> AddOneAsync(Song song);
        //Task ImportFromCS(nesto)


    }

}