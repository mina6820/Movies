using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class FavouriteSeries
    {
        public int Id { get; set; }

        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series Series { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
