namespace Movies.DTOs.Favourite
{
    public class FavSeriesDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }

        public string? SeriesDescription { get; set; }
        public string SeriesImage { get; set; }
    }
}
