using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.FavMovieRepo
{
    public interface IFavMovieRepository : IRepository<FavouriteMovie>
    {
        public List<FavouriteMovie> GetAllFavMoviesForUser(string userId);
    }
}