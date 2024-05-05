using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.Favourite;
using Movies.Models;
using Movies.Repositories.FavMovieRepo;

using Movies.Repositories.MovieRepo;

using System.Security.Claims;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavMovieController : ControllerBase
    {
        private readonly IFavMovieRepository favMovieRepository;
        private readonly IMovieRepository movieRepository;
        public FavMovieController(IFavMovieRepository favMovieRepository, IMovieRepository movieRepository)
        {
            this.favMovieRepository = favMovieRepository;
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetAll()
        {
            // Get the currently authenticated user's identity
            ClaimsPrincipal user = this.User;

            // Retrieve user's ID
            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<FavouriteMovie> favouriteMovies = favMovieRepository.GetAllFavMoviesForUser(userLginedId);
            List<FavMovieDTO> FavListDTO = new List<FavMovieDTO>();
            foreach (var item in favouriteMovies)
            {
                FavMovieDTO favMovieDTO = new FavMovieDTO()
                {
                    Id = item.Id,
                    MovieId = item.MovieID,
                    UserId = item.UserID,
                    MovieName = item.Movie.Title,
                    MovieImage = item.Movie.PosterImage,
                    MovieDescription = item.Movie.Description,

                };

                FavListDTO.Add(favMovieDTO);
            }
            return new GeneralResponse() { IsSuccess = true, Data = FavListDTO };


        }

        [HttpGet("{MovieID}")]
        [Authorize]
        public ActionResult<dynamic> IsFavorite(int MovieID)
        {
            Movie movie = movieRepository.GetById(MovieID);
            if (movie == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Movie" };
            }

            ClaimsPrincipal user = this.User;

            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool IsFavoriteMovie = favMovieRepository.IsFavorite(MovieID, userLginedId);
            //test
            if (IsFavoriteMovie)
            {
                return new GeneralResponse() { IsSuccess = true, Data = "Movie Found" };
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Movie Not Found" };
            }

        }

        [HttpPost("{MovieId:int}")]
        [Authorize]
        public ActionResult<dynamic> AddMovieToFavorite(int MovieId)
        {
            // Get the currently authenticated user's identity
            ClaimsPrincipal user = this.User;

            // Retrieve user's ID
            string userLginedId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Movie movie = movieRepository.GetById(MovieId);
            bool isFoundFavMovie = favMovieRepository.IsFavorite(MovieId, userLginedId);

            if (movie == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Movie" };
            }

            if (isFoundFavMovie)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Movie Already Exist" };

            }

            else
            {
                //// Get the currently authenticated user's identity
                //ClaimsPrincipal user = this.User;

                //// Retrieve user's ID
                //string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                FavouriteMovie favouriteMovie = new FavouriteMovie()
                {
                    MovieID= MovieId,
                    
                    UserID = userLginedId,
                };
                favMovieRepository.Insert(favouriteMovie);
                favMovieRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = " Added Successfully " };

            }
        }

        [HttpDelete("{MovieID}")]
        [Authorize]
        public ActionResult<dynamic> DeleteMovie(int MovieID)
        {
            ClaimsPrincipal user = this.User;
            string userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            bool isRemoved = favMovieRepository.RemoveMovieFromFevorite(MovieID,userId);

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
                    Data = "Movie Not Found",
                };
            }
        }

    }
}
