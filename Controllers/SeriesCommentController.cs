using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Repositories.MovieCommentRepo;
using Movies.Repositories.SeriesCommentRepo;
using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeriesCommentController : ControllerBase
    {
        private readonly ISeries_CommentRepo series_CommentRepo;

        public SeriesCommentController(ISeries_CommentRepo series_CommentRepo)
        {
            this.series_CommentRepo = series_CommentRepo;
        }
        [HttpPost("AddComment/{SeriesID}")]
        public IActionResult AddComment(int SeriesID, [FromBody] SeriesCommentInputModel inputModel)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            series_CommentRepo.AddComment(userId, SeriesID, inputModel.Comment);

            return Ok(new { Message = "Successfully Commented" });
        }

        public class SeriesCommentInputModel
        {
            public string Comment { get; set; }
        }


        [HttpGet("GetSeriesComments/{SeriesID}")]
        public ActionResult<List<SeriesComment>> GetSeriesComments(int SeriesID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var seriesComments = series_CommentRepo.GetAll_SeriesComments(SeriesID);
            return Ok(seriesComments);
        }

        [HttpGet("GetUserComments")]
        public ActionResult<List<SeriesComment>> GetUserComments()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var seriesComments = series_CommentRepo.GetAll_UserComments(userId);
            return Ok(seriesComments);
        }

        [HttpGet("GetspecificComment/{SeriesCommentID}")]
        public ActionResult<SeriesComment> GetspecificComment(int SeriesCommentID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var SeriesComment = series_CommentRepo.GetSpecific_Comment(SeriesCommentID);
            return Ok(SeriesComment);
        }

        [HttpPost("RemoveComment/{SeriesCommentID}")]
        public IActionResult RemoveComment(int SeriesCommentID)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            series_CommentRepo.RemoveSeriesComment(SeriesCommentID);
            return Ok(new { Message = "Removed Comment Successfully" });
        }

        [HttpGet("NumberCommentsOfSeries/{SeriesID}")]
        public IActionResult NumberCommentsOfMovie(int SeriesID)
        {
            var CommentsCount = series_CommentRepo.GetNumberofComment_Series(SeriesID);
            return Ok(new { SeriesID, CommentsCount });
        }

        [HttpGet("NumberCommentsOfUser")]
        public IActionResult NumberCommentsOfUser()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var CommentsCount = series_CommentRepo.GetNumberofComment_User(userId);
            return Ok(new { UserID = userId, CommentsCount });
        }
    }
}