using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.FavSeriesRepo
{
    public class FavSeriesRepository : Repository<FavouriteSeries>, IFavSeriesRepository
    {
        private readonly Context _context;
        public FavSeriesRepository(Context _context) : base(_context)
        {
            this._context = _context;
        }

        public List<FavouriteSeries> GetAllFavSeriesForUser(string userId)
        {
            return _context.FavouriteSeries.Include(f => f.Series).Where(fav => fav.UserID == userId).ToList();
        }

        public bool IsFavorite(int FavSeriesId, string UserLogginedId)
        {
            FavouriteSeries favouriteSeries = _context.FavouriteSeries.FirstOrDefault(fav => fav.SeriesID == FavSeriesId && fav.UserID == UserLogginedId);

            if (favouriteSeries == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool RemoveSeries(int Id)
        {
            FavouriteSeries Favseries = _context.FavouriteSeries.FirstOrDefault(Fav => Fav.Id == Id);

            if (Favseries != null)
            {
                _context.FavouriteSeries.Remove(Favseries);
                _context.SaveChanges(); // Make sure to save changes after removal
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveSeriesFromFevorite(int SeriesId , string UserId)
        {
            FavouriteSeries Favseries = _context.FavouriteSeries.FirstOrDefault(Fav => Fav.SeriesID== SeriesId && Fav.UserID == UserId);

            if (Favseries != null)
            {
                _context.FavouriteSeries.Remove(Favseries);
                _context.SaveChanges(); // Make sure to save changes after removal
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}