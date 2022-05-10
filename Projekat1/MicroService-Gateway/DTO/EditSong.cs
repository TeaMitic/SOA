using MicroService_Gateway.Models;

namespace MicroService_Gateway.DTO 
{
    public class EditSong 
    {
        public Song Song {get; set;}
        public string? Artist {get; set;}
        public string? Track {get; set;}
        public EditSong(Song song, string? artist, string? track)
        { 
            Song = song;
            Artist = artist;
            Track = track;
        }
    }
}