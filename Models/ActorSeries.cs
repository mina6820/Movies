using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class ActorSeries
    {
        public int Id { get; set; }

        [ForeignKey("Actor")]
        public int ActorID { get; set; }
        public Actor Actor { get; set; }


        [ForeignKey("Series")]
        public int SeriesID { get; set; }
        public Series Series { get; set; }
    }
}
