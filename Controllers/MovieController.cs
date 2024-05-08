using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.MovieRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult<GeneralResponse> AddMovie(AddMovieDTO movieDto)
        {
            if(ModelState.IsValid)
            {
                // here i sould sure from the direcor id that it exist , so i will wait the director untill be ready to sure and don't forget to map the object in the object initializer
                /////
                // mappint from dto to movie
                Movie movie = new Movie() { Title = movieDto.Title, Description = movieDto.Description, CreatedYear = movieDto.CreatedYear,
                                            FilmSection = movieDto.FilmSection, Quality = movieDto.Quality, LengthMinutes = movieDto.LengthMinutes,
                                             Revenue = movieDto.Revenue, PosterImage = movieDto.PosterImage,DirectorID = movieDto.DirectorID};
                movieRepository.Insert(movie);
                movieRepository.Save();
                return new GeneralResponse() { IsSuccess = true , Data = movie};
            }
            return new GeneralResponse() { IsSuccess = false, Data = "Invalid Data" };
        }


        [HttpGet]
        public ActionResult<GeneralResponse> GetAll()
        {
            List<Movie> moviesDb = movieRepository.GetAll();
            if (moviesDb.Count == 0)
                return new GeneralResponse() { IsSuccess = false, Data = "no movies" };
            List<MovieDTO> movies = new List<MovieDTO>();
            foreach (Movie movie in moviesDb)
            {
                MovieDTO movieDTO = new MovieDTO() { Id = movie.Id, Title = movie.Title, Description = movie.Description, PosterImage = movie.PosterImage
                                                     ,LengthMinutes = movie.LengthMinutes, FilmSection = movie.FilmSection, Quality = movie.Quality,
                                                      DirectorID = movie.Director.Id, CreatedYear = movie.CreatedYear , DirectorName = movie.Director.Name};
               
                movies.Add(movieDTO);
            }

            return new GeneralResponse() { IsSuccess = true, Data = movies };

        }

        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponse> GetById(int id)
        {
            Movie movieDb = movieRepository.GetMovieById(id);
            if (movieDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie with ID : {id}" };
            MovieDTO movie = new MovieDTO();
            movie.Id = movieDb.Id;
            movie.Title = movieDb.Title;
            movie.Description = movieDb.Description;
            movie.Revenue = movieDb.Revenue;
            movie.Quality = movieDb.Quality;
            movie.LengthMinutes = movieDb.LengthMinutes;
            movie.FilmSection = movieDb.FilmSection;
            movie.PosterImage = movieDb.PosterImage;
            movie.CreatedYear = movieDb.CreatedYear;
            movie.DirectorID = movieDb.Director.Id;
            movie.DirectorName = movieDb.Director.Name;
            return new GeneralResponse() { IsSuccess = true, Data = movie };
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<GeneralResponse> GetMovieByName(string name)
        {
            List<Movie> movies = movieRepository.GetMovieByName(name);
            if (movies.Count == 0)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movies with this Title : {name}" };
            List<MovieDTO> moviesDTO = new List<MovieDTO>();
            foreach(Movie movie in movies)
            {
                MovieDTO movieDTO = new MovieDTO();
                movieDTO.Id = movie.Id;
                movieDTO.Title = movie.Title;
                movieDTO.Description = movie.Description;
                movieDTO.Revenue = movie.Revenue;
                movieDTO.Quality = movie.Quality;
                movieDTO.LengthMinutes = movie.LengthMinutes;
                movieDTO.FilmSection = movie.FilmSection;
                movieDTO.PosterImage = movie.PosterImage;
                movieDTO.CreatedYear = movie.CreatedYear;
                movieDTO.DirectorID = movie.Director.Id;
                movieDTO.DirectorName = movie.Director.Name;

                moviesDTO.Add(movieDTO);
                
            }
            return new GeneralResponse() { IsSuccess = true, Data = moviesDTO };
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult<GeneralResponse> EditMovie(int id, AddMovieDTO MovieDTO)
        {
            Movie movieDb = movieRepository.GetMovieById(id);
            if (movieDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie With With ID : {id}" };
            movieDb.Title = MovieDTO.Title;
            movieDb.Description = MovieDTO.Description;
            movieDb.Quality = MovieDTO.Quality;
            movieDb.DirectorID = MovieDTO.DirectorID;
            movieDb.CreatedYear = MovieDTO.CreatedYear;
            movieDb.FilmSection = MovieDTO.FilmSection;
            movieDb.PosterImage = MovieDTO.PosterImage;
            movieDb.Revenue = MovieDTO.Revenue;
            movieDb.LengthMinutes = MovieDTO.LengthMinutes;

            movieRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = movieDb };
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult<GeneralResponse> DeleteMovie(int id)
        {
            Movie movieDb = movieRepository.GetMovieById(id);
            if (movieDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie With With ID : {id}" };
            movieDb.IsDeleted = true;
            movieRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data= "Movie Deleted Successfully" };
        }
    }
}
