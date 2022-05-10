using MicroService_Gateway.Models;

namespace MicroService_Gateway.DTO 
{
    public class SongAndLyrics 
    {
        private Song _song;
        private string? _lyrics;
        public SongAndLyrics(Song song, string? lyrics)
        { 
            _song = song;
            _lyrics = lyrics;
        }
    }
}