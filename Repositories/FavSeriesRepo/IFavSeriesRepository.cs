using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.FavSeriesRepo
{
    public interface IFavSeriesRepository : IRepository<FavouriteSeries>
    {
        public List<FavouriteSeries> GetAllFavSeriesForUser(string userId);
        public bool IsFavorite(int FavId);
        public bool RemoveSeries(int Id);
    }
}