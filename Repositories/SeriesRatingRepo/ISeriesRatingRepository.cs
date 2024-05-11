using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.SeriesRatingRepo
{
    public interface ISeriesRatingRepository : IRepository<SeriesRating>
    {
        public double GetSeriesRating(int SeriesID);
    }
}
