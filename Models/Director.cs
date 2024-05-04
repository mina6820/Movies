namespace Movies.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Age { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Overview { get; set; }

        public List<Movie>? Movies { get; set; }
        public List<Series>? Series { get; set; }


    }
}
