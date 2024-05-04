//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Movies.Authentication;
//using Movies.Models;
//using Movies.Repositories.MovieLikeRepo;
//using System.Collections.Generic;

//namespace Movies.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//  //  [Authorize] // Add authorization here

//    public class MovieLikeController : ControllerBase
//    {
//        private readonly IMovie_LikeRepo movieLikeRepository;

//        public MovieLikeController(IMovie_LikeRepo movieLikeRepository)
//        {
//            this.movieLikeRepository = movieLikeRepository;
//        }

//        [HttpPost("AddLike/{MovieID:int}")]
//        public ActionResult AddLike(int MovieID)
//        {
//            movieLikeRepository.UserLikesMovie(MovieID);
//            return Ok("Successfully Liked");
//        }

//        [HttpGet("GetMovieLikes/{MovieID:int}")]
//        public ActionResult GetMovieLikes(int MovieID)
//        {
//            List<AppUser> users = movieLikeRepository.GetMovieLikes(MovieID);
//            return Ok(users);
//        }

//        [HttpGet("GetUserLikes")]
//        public ActionResult GetUserLikes()
//        {
//            List<Movie> movies = movieLikeRepository.GetUserLikes();
//            return Ok(movies);
//        }

//        [HttpGet("GetspecificLike/{MovieID:int}")]
//        public ActionResult GetspecificLike(int MovieID)
//        {
//            MovieLike movieLike = movieLikeRepository.GetMovieLike(MovieID);
//            return Ok(movieLike);
//        }

//        [HttpPost("Dislike/{MovieID:int}")]
//        public ActionResult Dislike(int MovieID)
//        {
//            movieLikeRepository.DislikeMovie(MovieID);
//            return Ok("Disliked Successfully");
//        }
//    }
//}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Authentication;
using Movies.Models;
using Movies.Repositories.MovieLikeRepo;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieLikeController : ControllerBase
    {
        private readonly IMovie_LikeRepo movieLikeRepository;

        public MovieLikeController(IMovie_LikeRepo movieLikeRepository)
        {
            this.movieLikeRepository = movieLikeRepository;
        }

        [HttpPost("AddLike/{MovieID:int}")]
        public async Task<IActionResult> AddLike(int MovieID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            await movieLikeRepository.UserLikesMovie(userId, MovieID);

            return Ok("Successfully Liked");
        }

        [HttpGet("GetMovieLikes/{MovieID:int}")]
        public ActionResult<List<AppUser>> GetMovieLikes(int MovieID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var users = movieLikeRepository.GetMovieLikes(MovieID);
            return Ok(users);
        }

        [HttpGet("GetUserLikes")]
        public ActionResult<List<Movie>> GetUserLikes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var movies = movieLikeRepository.GetUserLikes(userId);
            return Ok(movies);
        }

        [HttpGet("GetspecificLike/{MovieID:int}")]
        public ActionResult<MovieLike> GetspecificLike(int MovieID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var movieLike = movieLikeRepository.GetMovieLike(MovieID, userId);
            return Ok(movieLike);
        }

        [HttpPost("Dislike/{MovieID:int}")]
        public IActionResult Dislike(int MovieID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            movieLikeRepository.DislikeMovie(MovieID, userId);
            return Ok("Disliked Successfully");
        }

        [HttpGet("NumberLikesOfMovie/{MovieID:int}")]
        public IActionResult NumberLikesOfMovie(int MovieID)
        {
            var likesCount = movieLikeRepository.NumberLikesOfMovie(MovieID);
            return Ok(new { MovieID, LikesCount = likesCount });
        }

        [HttpGet("NumberLikesOfUser")]
        public IActionResult NumberLikesOfUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var likesCount = movieLikeRepository.NumberLikesOfUser(userId);
            return Ok(new { UserID = userId, LikesCount = likesCount });
        }
    }
}
