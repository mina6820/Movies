using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs
{
    public class SeriesToGetDTO
    {
        
        public int SeriesId { get; set; }


        public string Title { get; set; }
        public string? Description { get; set; }
        public int? CreatedYear { get; set; }
        public string? PosterImage { get; set; }
        public int LengthMinutes { get; set; }
        
        public int Revenue { get; set; }
        public List<SeasonsDTO>? Seasons { get; set; }
       
        [Column(TypeName = "nvarchar(20)")]
        public FilmSection FilmSection { get; set; }

        
        [Column(TypeName = "nvarchar(20)")]
        public MovieQuality Quality { get; set; }

        
        public int DirectorID { get; set; }
        public string DirectorName { get; set; }
    }
}
