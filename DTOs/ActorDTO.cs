using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs
{
    public class ActorDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Age { get; set; }
        public string? Image { get; set; }
        public String? Overview { get; set; }
    }
}
