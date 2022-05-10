using CsvHelper.Configuration;

namespace MicroService_Gateway.Models
{
    
    public class Song { 
        public string? TrackName { get; set; }
        public string? ArtistName { get; set; }
        public string? Genre { get; set; }
        public int? BeatsPerMinute { get; set; }
        public int? Energy { get; set; }
        public int? Danceability { get; set; }
        public int? LoudnessIndB { get; set; }
        public int? Liveness { get; set; }
        public int? Valence { get; set; }
        public int? Lenght { get; set; }
        public int? Acousticness { get; set; }
        public int? Speechiness { get; set; }
        public int? Popularity { get; set; }

       
    }

    public class SongClassMap : ClassMap<Song> { 
        public SongClassMap() { 
            Map(s => s.TrackName).Name("Track_Name");
            Map(s => s.ArtistName).Name("Artist_Name");
            Map(s => s.Genre);
            Map(s => s.BeatsPerMinute).Name("Beats_Per_Minute");
            Map(s => s.Energy);
            Map(s => s.Danceability);
            Map(s => s.LoudnessIndB).Name("Loudness_dB");
            Map(s => s.Liveness);
            Map(s => s.Valence);
            Map(s => s.Lenght);
            Map(s => s.Acousticness);
            Map(s => s.Speechiness);
            Map(s => s.Popularity);
        }
    }
}