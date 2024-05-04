using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs
{
    public class SeriesDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? CreatedYear { get; set; }
        public string? PosterImage { get; set; }
        public int LengthMinutes { get; set; }
        public int Revenue { get; set; }
        public int DirectorID { get; set; }

        //public List<Season> seasons { get; set;}

        // Enum For (Anime, Arabic, Forigen, Assian...)
        //[Column(TypeName = "nvarchar(20)")]
        //public FilmSection FilmSection { get; set; }

        // The Enum Will be mapped as a string
        //[Column(TypeName = "nvarchar(20)")]
        //public MovieQuality Quality { get; set; }


    }
}
