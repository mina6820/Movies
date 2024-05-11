using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? CreatedYear { get; set; }
        public string? PosterImage { get; set; }

        public int? rating { get; set; }

        public int LengthMinutes { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Revenue { get; set; }

        // Enum For (Anime, Arabic, Forigen, Assian...)
        [Column(TypeName = "nvarchar(20)")]
        public FilmSection FilmSection { get; set; }

        // The Enum Will be mapped as a string
        [Column(TypeName = "nvarchar(20)")]
        public MovieQuality Quality { get; set; }

        [ForeignKey("Director")]
        public int DirectorID { get; set; }
        public Director Director { get; set; }
    }
}
