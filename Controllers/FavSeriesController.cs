using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.FavMovieRepo;
using Movies.Repositories.FavSeriesRepo;
using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavSeriesController : ControllerBase
    {
        private readonly IFavSeriesRepository favSeriesRepository;

        public FavSeriesController(IFavSeriesRepository favSeriesRepository)
        {
            this.favSeriesRepository = favSeriesRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetAll()
        {
            // Get the currently authenticated user's identity
            ClaimsPrincipal user = this.User;

            // Retrieve user's ID
            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<FavouriteSeries> favouriteSeries=favSeriesRepository.GetAllFavSeriesForUser(userLginedId);
            List<FavSeriesDTO> FavListDTO=new List<FavSeriesDTO>();
            foreach (var item in favouriteSeries)
            {
                FavSeriesDTO favSeriesDTO = new FavSeriesDTO()
                {
                    Id = item.Id,
                    SeriesId=item.SeriesID,
                    UserId=item.UserID,
                    SeriesName=item.Series.Title,
                    SeriesImage=item.Series.PosterImage

                };

                FavListDTO.Add(favSeriesDTO);
            }
            return new GeneralResponse() { IsSuccess=true , Data= FavListDTO };


        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<dynamic> IsFavorite( int id )
        {
            bool IsFavoriteSeries= favSeriesRepository.IsFavorite(id);
            return new GeneralResponse() { IsSuccess=true ,Data= IsFavoriteSeries };
        }

        [HttpPost("{SeriesId:int}")]
        [Authorize]
        public ActionResult<dynamic> AddSeriesToFavorite(int SeriesId)
        {
            if (SeriesId == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "plz enter valid Data" };
            }
            else
            {
                // Get the currently authenticated user's identity
                ClaimsPrincipal user = this.User;

                // Retrieve user's ID
                string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                FavouriteSeries favouriteSeries = new FavouriteSeries()
                {
                    SeriesID= SeriesId,
                    UserID = userId,
                };
                favSeriesRepository.Insert(favouriteSeries);
                favSeriesRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = favouriteSeries };

            }
        }

        [HttpDelete("{Id}")]
        [Authorize]
        public ActionResult<dynamic> DeleteSeries(int Id)
        {
            ClaimsPrincipal user = this.User;
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isRemoved = favSeriesRepository.RemoveSeries(Id);

            if (isRemoved)
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = "Removed Successfully",
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Series Not Found",
                };
            }
        }

    }
}
