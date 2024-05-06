using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs
{
    public class SeasonToGetDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public int NumOfEpisodes { get; set; }
        
        public int SeriesID { get; set; }
        public string SeriesName { get; set; }

    }
}
