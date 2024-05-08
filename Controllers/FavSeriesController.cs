using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.Favourite;
using Movies.Models;
using Movies.Repositories.FavMovieRepo;
using Movies.Repositories.FavSeriesRepo;
using Movies.Repositories.SeriesRepo;
using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavSeriesController : ControllerBase
    {
        private readonly IFavSeriesRepository favSeriesRepository;
        private readonly ISeriesRepository seriesRepository;
        public FavSeriesController(IFavSeriesRepository favSeriesRepository, ISeriesRepository seriesRepository)
        {
            this.favSeriesRepository = favSeriesRepository;
            this.seriesRepository = seriesRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetAll()
        {
            // Get the currently authenticated user's identity
            ClaimsPrincipal user = this.User;

            // Retrieve user's ID
            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<FavouriteSeries> favouriteSeries = favSeriesRepository.GetAllFavSeriesForUser(userLginedId);
            List<FavSeriesDTO> FavListDTO = new List<FavSeriesDTO>();
            foreach (var item in favouriteSeries)
            {
                FavSeriesDTO favSeriesDTO = new FavSeriesDTO()
                {
                    Id = item.Id,
                    SeriesId = item.SeriesID,
                    UserId = item.UserID,
                    SeriesName = item.Series.Title,
                    SeriesImage = item.Series.PosterImage,
                    SeriesDescription = item.Series.Description,

                };

                FavListDTO.Add(favSeriesDTO);
            }
            return new GeneralResponse() { IsSuccess = true, Data = FavListDTO };


        }

        [HttpGet("series/{SeriesID}")]
        [Authorize]
        public ActionResult<dynamic> IsFavorite(int SeriesID)
        {
            Series series = seriesRepository.GetById(SeriesID);
            if (series == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Series" };
            }

            ClaimsPrincipal user = this.User;

            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool IsFavoriteSeries = favSeriesRepository.IsFavorite(SeriesID, userLginedId);
            //test
            if (IsFavoriteSeries)
            {
                return new GeneralResponse() { IsSuccess = true, Data = "Seires Found" };
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Series Not Found" };
            }

        }

        [HttpPost("{SeriesId:int}")]
        [Authorize]
        public ActionResult<dynamic> AddSeriesToFavorite(int SeriesId)
        {
            // Get the currently authenticated user's identity
            ClaimsPrincipal user = this.User;

            // Retrieve user's ID
            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Series series = seriesRepository.GetById(SeriesId);
            bool isFoundFavSeries = favSeriesRepository.IsFavorite(SeriesId, userLginedId);

            if (series == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Series" };
            }

            if (isFoundFavSeries)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Series Already Exist" };

            }

            else
            {
                //// Get the currently authenticated user's identity
                //ClaimsPrincipal user = this.User;

                //// Retrieve user's ID
                //string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                FavouriteSeries favouriteSeries = new FavouriteSeries()
                {
                    SeriesID = SeriesId,
                    UserID = userLginedId,
                };
                favSeriesRepository.Insert(favouriteSeries);
                favSeriesRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = " Added Successfully " };

            }
        }

        [HttpDelete("{SeriesId}")]
        [Authorize]
        public ActionResult<dynamic> DeleteSeries(int SeriesId)
        {
            ClaimsPrincipal user = this.User;
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isRemoved = favSeriesRepository.RemoveSeriesFromFevorite(SeriesId , userId);

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