using Newtonsoft.Json;
using MicroService_Gateway.Models;
namespace  MicroService_Gateway.DTO 
{ 
    public class DTOHelper 
    { 
        public DTOHelper() { }

        public static Song? SongToDTO(string songString) 
        {
            Song? song = JsonConvert.DeserializeObject<Song>(songString);
            return song;
        } 
    }
}