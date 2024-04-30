using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class FavouriteMovie
    {
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public AppUser User { get; set; }
    }
}
