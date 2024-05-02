using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.FavMovieRepo
{
    public class FavMovieRepository : Repository<FavouriteMovie> , IFavMovieRepository
    {
        private readonly Context context;

        public FavMovieRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<FavouriteMovie> GetAllFavMoviesForUser(string userId)
        {
            return context.FavouriteMovies.Where(fav=> fav.UserID == userId).ToList();
        }
    }
}
