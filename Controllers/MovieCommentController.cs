//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Movies.Authentication;
//using Movies.Models;
//using Movies.Repositories.MovieLikeRepo;
//using Movies.Repositories.SeriesCommentRepo;
//using System.Security.Claims;

//namespace Movies.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class MovieCommentController : ControllerBase
//    {
//        private readonly IMovie_CommentRepo movie_CommentRepo;

//        public MovieCommentController( IMovie_CommentRepo movie_CommentRepo)
//        {
//            this.movie_CommentRepo = movie_CommentRepo;
//        }



//        [HttpPost("AddComment/{MovieID:int ,comment:alpha }")]
//        public  IActionResult AddComment(int MovieID , string comment)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//             movie_CommentRepo.AddComment(userId, MovieID , comment);

//            return Ok("Successfully Commented");
//        }

//        [HttpGet("GetMovieComments/{MovieID:int}")]
//        public ActionResult<List<MovieComment>> GetMovieComments(int MovieID)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//            var movieComments = movie_CommentRepo.GetAll_MovieComments(MovieID);
//            return Ok(movieComments);
//        }

//        [HttpGet("GetUserComments")]
//        public ActionResult<List<MovieComment>> GetUserComments()
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//            var movieComments = movie_CommentRepo.GetAll_UserComments(userId);
//            return Ok(movieComments);
//        }

//        [HttpGet("GetspecificComment/{MovieCommentID:int}")]
//        public ActionResult<MovieComment> GetspecificComment(int MovieCommentID)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//            var movieComment = movie_CommentRepo.GetSpecific_Comment(MovieCommentID);
//            return Ok(movieComment);
//        }

//        [HttpPost("RemoveComment/{MovieCommentID:int}")]
//        public IActionResult RemoveComment(int MovieCommentID)
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//            movie_CommentRepo.RemoveMovieComment(MovieCommentID);
//            return Ok("Removed Comment Successfully");
//        }

//        [HttpGet("NumberCommentsOfMovie/{MovieID:int}")]
//        public IActionResult NumberCommentsOfMovie(int MovieID)
//        {
//            var CommentsCount = movie_CommentRepo.GetNumberofComment_Movie(MovieID);
//            return Ok(new { MovieID, CommentsCount = CommentsCount  });
//        }

//        [HttpGet("NumberCommentsOfUser")]
//        public IActionResult NumberCommentsOfUser()
//        {
//            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            if (userId == null)
//            {
//                return Unauthorized(); // Handle unauthorized access
//            }

//            var CommentsCount = movie_CommentRepo.GetNumberofComment_User(userId);
//            return Ok(new { UserID = userId, CommentsCount = CommentsCount });
//        }

//    }
//}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Repositories.MovieCommentRepo;
using System.Collections.Generic;
using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieCommentController : ControllerBase
    {
        private readonly IMovie_CommentRepo movie_CommentRepo;

        public MovieCommentController(IMovie_CommentRepo movie_CommentRepo)
        {
            this.movie_CommentRepo = movie_CommentRepo;
        }

        //[HttpPost("AddComment/{MovieID}")]
        //public IActionResult AddComment(int MovieID, [FromBody] string comment)
        //{
        //    var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return Unauthorized(); // Handle unauthorized access
        //    }

        //    movie_CommentRepo.AddComment(userId, MovieID, comment);

        //    return Ok(new { Message = "Successfully Commented" });
        //}



        [HttpPost("AddComment/{MovieID}")]
        public IActionResult AddComment(int MovieID, [FromBody] MovieCommentInputModel inputModel)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            movie_CommentRepo.AddComment(userId, MovieID, inputModel.Comment);

            return Ok(new { Message = "Successfully Commented" });
        }

        public class MovieCommentInputModel
        {
            public string Comment { get; set; }
        }


        [HttpGet("GetMovieComments/{MovieID}")]
        public ActionResult<List<MovieComment>> GetMovieComments(int MovieID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var movieComments = movie_CommentRepo.GetAll_MovieComments(MovieID);
            return Ok(movieComments);
        }

        [HttpGet("GetUserComments")]
        public ActionResult<List<MovieComment>> GetUserComments()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var movieComments = movie_CommentRepo.GetAll_UserComments(userId);
            return Ok(movieComments);
        }

        [HttpGet("GetspecificComment/{MovieCommentID}")]
        public ActionResult<MovieComment> GetspecificComment(int MovieCommentID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var movieComment = movie_CommentRepo.GetSpecific_Comment(MovieCommentID);
            return Ok(movieComment);
        }

        [HttpPost("RemoveComment/{MovieCommentID}")]
        public IActionResult RemoveComment(int MovieCommentID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            movie_CommentRepo.RemoveMovieComment(MovieCommentID);
            return Ok(new { Message = "Removed Comment Successfully" });
        }

        [HttpGet("NumberCommentsOfMovie/{MovieID}")]
        public IActionResult NumberCommentsOfMovie(int MovieID)
        {
            var CommentsCount = movie_CommentRepo.GetNumberofComment_Movie(MovieID);
            return Ok(new { MovieID, CommentsCount });
        }

        [HttpGet("NumberCommentsOfUser")]
        public IActionResult NumberCommentsOfUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var CommentsCount = movie_CommentRepo.GetNumberofComment_User(userId);
            return Ok(new { UserID = userId, CommentsCount });
        }
    }
}
