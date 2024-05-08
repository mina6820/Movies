using Movies.Authentication;
using Movies.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs.RatingDTOs
{
    public class AddMovieRatingDTO
    {
        // From 1 : 5
        public int Rate { get; set; }

        [Required]
        public string UserID { get; set; }

        public int MovieID { get; set; }
    }
}
