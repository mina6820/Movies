namespace Movies.DTOs.Favourite
{
    public class FavMovieDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }

        public string? MovieDescription { get; set; }
        public string MovieImage { get; set; }
    }
}
