using System.ComponentModel.DataAnnotations.Schema;
using Movies.Authentication;

namespace Movies.Models
{
    public class MovieLike
    {
        public int Id { get; set; }


        [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }


        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
