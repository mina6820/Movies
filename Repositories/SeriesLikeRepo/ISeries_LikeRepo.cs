using Movies.Authentication;
using Movies.Models;

namespace Movies.Repositories.SeriesLikeRepo
{
    public interface ISeries_LikeRepo
    {
        public bool isUser_likes_Series(string userId, int SeriesID);
        public Task UserLikesSeries(string userId, int SeriesID);
        public List<AppUser> GetSeriesLikes(int SeriesID);
        public List<Series> GetUserLikes(string userId);
        public SeriesLike GetSeriesLike(int SeriesID, string userId);
        public void DislikeSeries(int SeriesID, string userId);
        public int NumberLikesOfSeries(int SeriesID);
        public int NumberLikesOfUser(string userId);
    }
}