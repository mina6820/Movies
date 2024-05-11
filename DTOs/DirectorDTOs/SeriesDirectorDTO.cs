using Movies.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.DTOs.DirectorDTOs
{
    public class SeriesDirectorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? PosterImage { get; set; }
    }
}
