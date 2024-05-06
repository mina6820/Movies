﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.CategoryMovieRepo;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.MovieRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryMovieController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMovieRepository movieRepository;
        private readonly ICategoryMovieRepository categoryMovieRepository;

        public CategoryMovieController(ICategoryRepository categoryRepository , IMovieRepository movieRepository, ICategoryMovieRepository categoryMovieRepository)
        {
            this.categoryRepository = categoryRepository;
            this.movieRepository = movieRepository;
            this.categoryMovieRepository = categoryMovieRepository;
        }

        [HttpPost("{movieId:int}/{categoryId:int}")]
        public ActionResult<GeneralResponse> AddMovieInCategory(int movieId, int categoryId)
        {
            // Fetch the movie by its ID
            Movie movie = movieRepository.GetMovieById(movieId);
            if (movie == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie With ID : {movieId}" };

            // Fetch the category by its ID
            Category category = categoryRepository.GetCategoryById(categoryId);
            if (category == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Category With ID : {categoryId}" };

            // sure if the movie is not exist in this category already 
            List<Movie> moviesDb = categoryMovieRepository.GetMoviesByCategoryId(categoryId);
            if (moviesDb.Contains(movie))
                return new GeneralResponse() { IsSuccess = false, Data = "This Movie already in this Category" };


            // Create a new entry in CategoryMovie class
            CategoryMovie categoryMovie = new CategoryMovie()
            {
                Movie = movie,
                MovieID = movieId,
                Category = category,
                CategoryID = categoryId
            };

            // Insert the new entry into the repository
            categoryMovieRepository.Insert(categoryMovie);
            categoryMovieRepository.Save();

            return new GeneralResponse() { IsSuccess = true, Data = "Added Successfully" };
        }

        [HttpGet("{categoryId:int}")]
        public ActionResult<GeneralResponse> getAllMoviesForCategory(int categoryId)
        {
            // get all movies for this category
            List<Movie> moviesDb = categoryMovieRepository.GetMoviesByCategoryId(categoryId);
            
            // sure that the list is not empty
            if (moviesDb.Count == 0)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Movie in Category ID : {categoryId}" };
            List<MovieDTO> movies = new List<MovieDTO>();
            // mapping from moviesDb to movies
            foreach (Movie movie in moviesDb)
            {
                MovieDTO movieDTO = new MovieDTO()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    CreatedYear = movie.CreatedYear,
                    PosterImage = movie.PosterImage,                                                    
                    LengthMinutes = movie.LengthMinutes,
                    FilmSection = movie.FilmSection,
                    Quality = movie.Quality,
                    DirectorID = movie.DirectorID,
                    DirectorName = movie.Director.Name
                };

                movies.Add(movieDTO);
            }
            return new GeneralResponse() { IsSuccess = true, Data = movies };
        }





    }
}
