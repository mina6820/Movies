namespace Movies.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Series> Series { get; set; }

    }
}
