using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Authentication;
using Movies.Models;
using Movies.Repositories.MovieLikeRepo;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Movies.Repositories.SeriesLikeRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
    public class SeriesLikeController : ControllerBase
    {
        private readonly ISeries_LikeRepo seriesLikeRepo;
        public SeriesLikeController(ISeries_LikeRepo series_LikeRepo)
        {
            this.seriesLikeRepo = series_LikeRepo;
        }
        [HttpPost("AddLike/{SeriesID:int}")]
        public async Task<IActionResult> AddLike(int SeriesID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            await seriesLikeRepo.UserLikesSeries(userId, SeriesID);

            return Ok("Successfully Liked");
        }

        [HttpGet("GetSeriesLikes/{SeriesID:int}")]
        public ActionResult<List<AppUser>> GetSeriesLikes(int SeriesID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var users = seriesLikeRepo.GetSeriesLikes(SeriesID);
            return Ok(users);
        }

        [HttpGet("GetUserLikes")]
        public ActionResult<List<Series>> GetUserLikes()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var series = seriesLikeRepo.GetUserLikes(userId);
            return Ok(series);
        }

        [HttpGet("GetspecificLike/{SeriesID:int}")]
        public ActionResult<SeriesLike> GetspecificLike(int SeriesID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var seriesLike = seriesLikeRepo.GetSeriesLike(SeriesID, userId);
            return Ok(seriesLike);
        }

        [HttpPost("Dislike/{SeriesID:int}")]
        public IActionResult Dislike(int SeriesID)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            seriesLikeRepo.DislikeSeries(SeriesID, userId);
            return Ok("Disliked Successfully");
        }

        [HttpGet("NumberLikesOfSeries/{SeriesID:int}")]
        public IActionResult NumberLikesOfSeries(int SeriesID)
        {
            var likesCount = seriesLikeRepo.NumberLikesOfSeries(SeriesID);
            return Ok(new { SeriesID, LikesCount = likesCount });
        }

        [HttpGet("NumberLikesOfUser")]
        public IActionResult NumberLikesOfUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            var likesCount = seriesLikeRepo.NumberLikesOfUser(userId);
            return Ok(new { UserID = userId, LikesCount = likesCount });
        }
    }
}
