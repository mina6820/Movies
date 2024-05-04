using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs.ActorDTOs
{
    public class ActorDTO
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Age { get; set; }
        public string? Image { get; set; }
        public string? Overview { get; set; }
    }
}
