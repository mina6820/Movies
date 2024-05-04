using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs
{
    public class AddMovieDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? CreatedYear { get; set; }
        public string? PosterImage { get; set; }
        public int LengthMinutes { get; set; }
        public int Revenue { get; set; }
        public FilmSection FilmSection { get; set; }
        public MovieQuality Quality { get; set; }
        public int DirectorID { get; set; }
    }
}
