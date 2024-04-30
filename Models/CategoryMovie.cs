using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class CategoryMovie
    {
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MovieID { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
