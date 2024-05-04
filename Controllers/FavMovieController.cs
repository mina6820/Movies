using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.FavMovieRepo;
using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavMovieController : ControllerBase
    {
        private readonly IFavMovieRepository favMovieRepository;

        public FavMovieController(IFavMovieRepository favMovieRepository)
        {
            this.favMovieRepository = favMovieRepository;
        }

        [HttpPost("{MovieId:int}")]
        public ActionResult<dynamic> AddMovieToFavorite(int MovieId)
        {
            if (MovieId == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "plz enter valid Data" };
            }else
            {
                // Get the currently authenticated user's identity
                ClaimsPrincipal user = this.User;

                // Retrieve user's ID
                string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                FavouriteMovie favouriteMovie = new FavouriteMovie() { 
                MovieID= MovieId,
                UserID= userId,
                };
                favMovieRepository.Insert(favouriteMovie);
                favMovieRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = favouriteMovie };

            }
        }
    }
}
