using MicroService_Gateway.Models;

namespace MicroService_Gateway.Repository 
{
    public class SongRepository : ISongRepository
    {
        public Task AddOne(Song song)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOne(string artist, string track)
        {
            throw new NotImplementedException();
        }

        public Task EditOne(string artist, string track, Song updateObject)
        {
            throw new NotImplementedException();
        }

        public Task<Song> GetOne(string artist, string track)
        {
            throw new NotImplementedException();
        }
    }
}