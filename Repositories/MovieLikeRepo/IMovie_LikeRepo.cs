//using Movies.Authentication;
//using Movies.Models;

//namespace Movies.Repositories.MovieLikeRepo
//{
//    public interface IMovie_LikeRepo
//    {
//        public bool isUser_likes_Movie(int MovieID);
//        public Task UserLikesMovie(int MovieID);

//        public List<AppUser> GetMovieLikes(int MovieID);

//        public List<Movie> GetUserLikes();

//        public MovieLike GetMovieLike(int MovieID);

//        public void DislikeMovie(int MovieID);

//        public int NumberLikesOfMovie(int movieId);

//        public int NumberLikesOfUser();

//        public string GetUserId();

//        public void save();

//    }
//}
using Movies.Authentication;
using Movies.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Repositories.MovieLikeRepo
{
    public interface IMovie_LikeRepo
    {
     public   bool isUser_likes_Movie(string userId, int MovieID);
     public   Task UserLikesMovie(string userId, int MovieID);
     public   List<AppUser> GetMovieLikes(int MovieID);
      public  List<Movie> GetUserLikes(string userId);
     public   MovieLike GetMovieLike(int MovieID, string userId);
      public  void DislikeMovie(int MovieID, string userId);
      public  int NumberLikesOfMovie(int movieId);
        public  int NumberLikesOfUser(string userId);
    }
}
