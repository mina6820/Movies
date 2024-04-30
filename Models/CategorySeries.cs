using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class CategorySeries
    {
        public int Id { get; set; }

        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series Series { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
