using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs.ActorDTOs;
using Movies.DTOs.RatingDTOs;
using Movies.Models;
using Movies.Repositories.MovieRatingRepo;
using Movies.Repositories.MovieRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingController : ControllerBase
    {
        private readonly IMovieRatingRepository movieRatingRepository;
        private readonly IMovieRepository movieRepository;

        public MovieRatingController(IMovieRatingRepository _movieRatingRepository)
        {
            movieRatingRepository = _movieRatingRepository;
            this.movieRepository = movieRepository;
        }


        [HttpGet("{id}")]
        public ActionResult<GeneralResponse> GetMovieRating(int id)
        {
            Movie movieDb = movieRepository.GetMovieById(id);
            if (movieDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie with ID : {id}" };


            double movieRating = movieRatingRepository.GetMovieRating(id);
            return new GeneralResponse() { IsSuccess = true, Data = movieRating };
        }

        [HttpPost]
        public ActionResult<GeneralResponse> AddRating(AddMovieRatingDTO addRatingDTO) 
        {
            if(ModelState.IsValid) 
            {
                MovieRating movieRating = new MovieRating()
                {
                    Rate = addRatingDTO.Rate,
                    UserID = addRatingDTO.UserID,
                    MovieID = addRatingDTO.MovieID,
                    IsDeleted = false,
                };
                movieRatingRepository.Insert(movieRating);
                movieRatingRepository.Save();

                return new GeneralResponse() { IsSuccess = true, Data = addRatingDTO };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = " Add Operation Failed "
                };
            }
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<GeneralResponse> EditRating(int id, AddMovieRatingDTO addRatingDTO) 
        {
            MovieRating movieRating = movieRatingRepository.GetById(id);
            if(movieRating == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "No Rating Found" };
            }
            movieRating.Rate = addRatingDTO.Rate;
            movieRating.UserID = addRatingDTO.UserID;
            movieRating.MovieID = addRatingDTO.MovieID;
            
            movieRatingRepository.Update(movieRating);
            movieRatingRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = addRatingDTO };
        }

        [HttpDelete("{id}")]
        public ActionResult<GeneralResponse> DeleteRating(int id)
        {
            var existingRating = movieRatingRepository.GetById(id);

            if (existingRating == null)
            {
                return NotFound();
            }

            movieRatingRepository.Delete(existingRating.Id);
            movieRatingRepository.Save();

            return new GeneralResponse() { IsSuccess = true, Data = $"Rating with id {id} has been deleted." };
        }
    }
}
