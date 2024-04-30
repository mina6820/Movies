using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class SeriesRating
    {
        public int Id { get; set; }

        // From 1 : 5
        public int Rate { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }


        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series series { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
