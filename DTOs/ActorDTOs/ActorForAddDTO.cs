using System.ComponentModel.DataAnnotations;

namespace Movies.DTOs.ActorDTOs
{
    public class ActorForAddDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Age { get; set; }
        public string? Image { get; set; }
        public string? Overview { get; set; }
    }
}
