using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.FavMovieRepo
{
    public interface IFavMovieRepository : IRepository<FavouriteMovie>
    {
        public List<FavouriteMovie> GetAllFavMoviesForUser(string userId);
        public bool IsFavorite(int FavMovieId, string UserLogginedId);
        public bool RemoveMovie(int Id);
        public bool RemoveMovieFromFevorite(int SeriesId, string UserId);
    }
}