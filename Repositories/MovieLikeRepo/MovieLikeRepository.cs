//using Instagram_Clone.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Movies.Authentication;
//using Movies.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace Movies.Repositories.MovieLikeRepo
//{
//    public class MovieLikeRepository : Repository<MovieLike>, IMovie_LikeRepo
//    {
//        private readonly Context context;
//        private readonly UserManager<AppUser> userManager;
//        private readonly IHttpContextAccessor httpContextAccessor;

//        public MovieLikeRepository(Context _context, UserManager<AppUser> _userManager, IHttpContextAccessor _httpContextAccessor) : base(_context)
//        {
//            context = _context;
//            userManager = _userManager;
//            httpContextAccessor = _httpContextAccessor;
//        }

//        public bool isUser_likes_Movie(int MovieID)
//        {
//            string userId = GetUserId();
//            return context.MovieLikes.Any(l => l.UserID == userId && l.MovieID == MovieID);
//        }

//        public async Task UserLikesMovie(int MovieID)
//        {
//            string userId = GetUserId();
//            Movie movie = context.Movies.FirstOrDefault(m => m.Id == MovieID);
//            AppUser user = context.Users.FirstOrDefault(u => u.Id == userId);

//            if (user != null && movie != null && !isUser_likes_Movie(MovieID))
//            {
//                var movieLike = new MovieLike
//                {
//                    UserID = userId,
//                    MovieID = MovieID
//                };
//                context.MovieLikes.Add(movieLike);
//                await context.SaveChangesAsync();
//            }
//        }

//        public List<AppUser> GetMovieLikes(int MovieID)
//        {
//            string userId = GetUserId();
//            return context.MovieLikes
//                .Where(l => l.MovieID == MovieID && !l.IsDeleted)
//                .Select(l => l.User)
//                .ToList();
//        }

//        public List<Movie> GetUserLikes()
//        {
//            string userId = GetUserId();
//            return context.MovieLikes
//                .Where(l => l.UserID == userId && !l.IsDeleted)
//                .Select(l => l.Movie)
//                .ToList();
//        }

//        public MovieLike GetMovieLike(int MovieID)
//        {
//            string userId = GetUserId();
//            return context.MovieLikes
//                .FirstOrDefault(ml => ml.UserID == userId && ml.MovieID == MovieID && !ml.IsDeleted);
//        }

//        public void DislikeMovie(int MovieID)
//        {
//            string userId = GetUserId();
//            MovieLike movieLike = context.MovieLikes
//                .FirstOrDefault(ml => ml.UserID == userId && ml.MovieID == MovieID && !ml.IsDeleted);
//            if (movieLike != null)
//            {
//                movieLike.IsDeleted = true;
//                context.SaveChanges();
//            }
//        }

//        public int NumberLikesOfMovie(int movieId)
//        {
//            return context.MovieLikes.Count(ml => ml.MovieID == movieId && !ml.IsDeleted);
//        }

//        public int NumberLikesOfUser()
//        {
//            string userId = GetUserId();
//            return context.MovieLikes.Count(ml => ml.UserID == userId && !ml.IsDeleted);
//        }

//        public string GetUserId()
//        {
//            ClaimsPrincipal userClaims = httpContextAccessor.HttpContext.User;
//            Claim claim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
//            return claim?.Value;
//        }

//        public void save()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}



// MovieLikeRepository.cs

using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Authentication;
using Movies.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Repositories.MovieLikeRepo
{
    public class MovieLikeRepository : Repository<MovieLike>, IMovie_LikeRepo
    {
        private readonly Context context;

        public MovieLikeRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public bool isUser_likes_Movie(string userId, int MovieID)
        {
            return context.MovieLikes.Any(l => l.UserID == userId && l.MovieID == MovieID);
        }

        public async Task UserLikesMovie(string userId, int MovieID)
        {
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == MovieID);
            AppUser user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null && movie != null && !isUser_likes_Movie(userId, MovieID))
            {
                var movieLike = new MovieLike
                {
                    UserID = userId,
                    MovieID = MovieID
                };
                context.MovieLikes.Add(movieLike);
                await context.SaveChangesAsync();
            }
        }

        public List<AppUser> GetMovieLikes(int MovieID)
        {
            return context.MovieLikes
                .Where(l => l.MovieID == MovieID && !l.IsDeleted)
                .Select(l => l.User)
                .ToList();
        }

        public List<Movie> GetUserLikes(string userId)
        {
            return context.MovieLikes
                .Where(l => l.UserID == userId && !l.IsDeleted)
                .Select(l => l.Movie)
                .ToList();
        }

        public MovieLike GetMovieLike(int MovieID, string userId)
        {
            return context.MovieLikes
                .FirstOrDefault(ml => ml.UserID == userId && ml.MovieID == MovieID && !ml.IsDeleted);
        }

        public void DislikeMovie(int MovieID, string userId)
        {
            MovieLike movieLike = context.MovieLikes
                .FirstOrDefault(ml => ml.UserID == userId && ml.MovieID == MovieID && !ml.IsDeleted);
            if (movieLike != null)
            {
                movieLike.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public int NumberLikesOfMovie(int movieId)
        {
            return context.MovieLikes.Count(ml => ml.MovieID == movieId && !ml.IsDeleted);
        }

        public int NumberLikesOfUser(string userId)
        {
            return context.MovieLikes.Count(ml => ml.UserID == userId && !ml.IsDeleted);
        }
    }
}
