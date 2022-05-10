using MicroService_Gateway.Models;

namespace MicroService_Gateway.DTO 
{
    public class SongAndLyrics 
    {
        public Song Song {get; set;}
        public string? Lyrics {get; set;}

        public SongAndLyrics(Song song, string? lyrics)
        { 
            Song = song;
            Lyrics = lyrics;
        }
    }
}