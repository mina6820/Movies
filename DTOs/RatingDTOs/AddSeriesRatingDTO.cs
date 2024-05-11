using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs.RatingDTOs
{
    public class AddSeriesRatingDTO
    {
        // From 1 : 5
        public int Rate { get; set; }

        [Required]
        public string UserID { get; set; }

        public int SeriesID { get; set; }
    }
}
