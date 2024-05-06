using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
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
            return context.FavouriteMovies.Include(f => f.Movie).Where(fav => fav.UserID == userId).ToList();
        }

        public bool IsFavorite(int FavMovieId, string UserLogginedId)
        {
            FavouriteMovie favouriteMovie = context.FavouriteMovies.FirstOrDefault(fav => fav.MovieID == FavMovieId && fav.UserID == UserLogginedId);

            if (favouriteMovie == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RemoveMovie(int Id)
        {
            FavouriteMovie favouriteMovie = context.FavouriteMovies.FirstOrDefault(Fav => Fav.Id == Id);

            if (favouriteMovie != null)
            {
                context.FavouriteMovies.Remove(favouriteMovie);
                context.SaveChanges(); // Make sure to save changes after removal
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveMovieFromFevorite(int MovieId, string UserId)
        {
            FavouriteMovie favouriteMovie = context.FavouriteMovies.FirstOrDefault(Fav => Fav.MovieID == MovieId && Fav.UserID == UserId);

            if (favouriteMovie != null)
            {
                context.FavouriteMovies.Remove(favouriteMovie);
                context.SaveChanges(); // Make sure to save changes after removal
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
