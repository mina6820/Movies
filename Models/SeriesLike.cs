using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class SeriesLike
    {
        public int Id { get; set; }


        [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }


        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series Series { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
