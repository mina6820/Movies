using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.MovieRatingRepo
{
    public interface IMovieRatingRepository : IRepository<MovieRating>
    {
        public double GetMovieRating(int MovieID);
    }
}
