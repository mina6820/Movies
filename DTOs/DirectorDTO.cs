using Movies.Models;
namespace Movies.DTOs
{
    public class DirectorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Age { get; set; }
        public string? Image { get; set; }
        public string? Overview { get; set; }
        public List<MoviesDirectorDTO> Movies { get; set; } = new List<MoviesDirectorDTO>();
        public List<string> Series { get; set; } = new List<string>();
    }

}