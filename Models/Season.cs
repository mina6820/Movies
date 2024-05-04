using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Season
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int NumOfEpisodes { get; set; }
        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series Series{ get; set; }
    }
}
