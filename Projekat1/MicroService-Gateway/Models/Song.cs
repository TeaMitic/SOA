using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration;

namespace MicroService_Gateway.Models
{
    
    public class Song { 
        [Required]
        public string? TrackName { get; set; }
        [Required]
        public string? ArtistName { get; set; }
        [Required]
        public string? Genre { get; set; }
        [Required]
        public int? BeatsPerMinute { get; set; }
        [Required]
        public int? Energy { get; set; }
        [Required]
        public int? Danceability { get; set; }
        [Required]
        public int? LoudnessIndB { get; set; }
        [Required]
        public int? Liveness { get; set; }
        [Required]
        public int? Valence { get; set; }
        [Required]
        public int? Length { get; set; }
        [Required]
        public int? Acousticness { get; set; }
        [Required]
        public int? Speechiness { get; set; }
        [Required]
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
            Map(s => s.Length);
            Map(s => s.Acousticness);
            Map(s => s.Speechiness);
            Map(s => s.Popularity);
        }
    }
}