using Instagram_Clone.Repositories;
using Movies.Authentication;
using Movies.Models;
using Movies.Repositories.MovieLikeRepo;

namespace Movies.Repositories.SeriesLikeRepo
{
    public class SeriesLikeRepo : Repository<SeriesLike>, ISeries_LikeRepo
    {
        private readonly Context context;

        public SeriesLikeRepo(Context _context) : base(_context)
        {
            context = _context;
        }
        public bool isUser_likes_Series(string userId, int SeriesID)
        {
            return context.SeriesLikes.Any(l => l.UserID == userId && l.SeriesID == SeriesID);
        }

        public async Task UserLikesSeries(string userId, int SeriesID)
        {
            Series series = context.Series.FirstOrDefault(m => m.Id == SeriesID);
            AppUser user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null && series != null && !isUser_likes_Series(userId, SeriesID))
            {
                var seriesLike = new SeriesLike
                {
                    UserID = userId,
                    SeriesID = SeriesID
                };
                context.SeriesLikes.Add(seriesLike);
                await context.SaveChangesAsync();
            }
        }

        public List<AppUser> GetSeriesLikes(int SeriesID)
        {
            return context.SeriesLikes
                .Where(l => l.SeriesID == SeriesID && !l.IsDeleted)
                .Select(l => l.User)
                .ToList();
        }

        public List<Series> GetUserLikes(string userId)
        {
            return context.SeriesLikes
                .Where(l => l.UserID == userId && !l.IsDeleted)
                .Select(l => l.Series)
                .ToList();
        }

        public SeriesLike GetSeriesLike(int SeriesID, string userId)
        {
            return context.SeriesLikes
                .FirstOrDefault(ml => ml.UserID == userId && ml.SeriesID == SeriesID && !ml.IsDeleted);
        }

        public void DislikeSeries(int SeriesID, string userId)
        {
            SeriesLike seriesLike = context.SeriesLikes
                .FirstOrDefault(ml => ml.UserID == userId && ml.SeriesID == SeriesID && !ml.IsDeleted);
            if (seriesLike != null)
            {
                seriesLike.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public int NumberLikesOfSeries(int SeriesID)
        {
            return context.SeriesLikes.Count(ml => ml.SeriesID == SeriesID && !ml.IsDeleted);
        }

        public int NumberLikesOfUser(string userId)
        {
            return context.SeriesLikes.Count(ml => ml.UserID == userId && !ml.IsDeleted);
        }
    }
}


