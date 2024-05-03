using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;

using Movies.Models;
using Movies.Repositories.ActroRepo;
using Movies.Repositories.DirectorRepo;
using System.Collections.Immutable;
namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DirectorController : ControllerBase
    {
        IDirectorRepository DirectorRepository;
        public DirectorController(IDirectorRepository _DirectorRepository)
        {
            DirectorRepository = _DirectorRepository;
        }


        [HttpPost]
        public ActionResult<dynamic> AddDirector(DirectorDTO directorDTO)
        {
            if (ModelState.IsValid)
            {
                Director director  = new Director()
                {
                    Age = directorDTO.Age,
                    Id = directorDTO.Id,
                    Image = directorDTO.Image,
                    Name = directorDTO.Name,
                    Overview = directorDTO.Overview,
                    
                };

                director.Movies = directorDTO.Movies.Select(m => new Movie()
                {
                    Title = m.Title,
                    CreatedYear = m.CreatedYear,
                    Description = m.Description,
                    FilmSection = m.FilmSection,
                    LengthMinutes = m.LengthMinutes,
                    PosterImage = m.PosterImage,
                    Quality = m.Quality,
                    Revenue = m.Revenue,
                    DirectorID = m.DirectorId
                   
                }).ToList();

                director.Series = directorDTO.Series.Select(s => new Series()
                {
                    Title = s.Title,
                    CreatedYear = s.CreatedYear,
                    Description = s.Description,
                    FilmSection = s.FilmSection,
                    LengthMinutes = s.LengthMinutes,
                    PosterImage = s.PosterImage,
                    Quality = s.Quality,
                    Revenue = s.Revenue,
                    DirectorID = s.DirectorID

                }).ToList();


                DirectorRepository.Insert(director);
                DirectorRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = director };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = " Addition Operation Failed "
                };
            }

            //return CreatedAtAction(nameof(GetActorById), new { id = actor.ID }, actor);
        }


        [HttpGet("{id}")]
        public ActionResult<dynamic> GetDirectorById(int id)
        {
            Director director = DirectorRepository.GetDirectorById(id);
            if (director == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Directors , Pleast Enter Valid Id"
                };
            }

            DirectorDTO directorDTO = new DirectorDTO
            {
                Id = director.Id,
                Name = director.Name,
                Age = director.Age,
                Image = director.Image,
                Overview = director.Overview,
                Movies = director.Movies.Select(m => new MoviesDirectorDTO
                {
                    Title = m.Title,
                    CreatedYear = m.CreatedYear,
                    Description = m.Description,
                    FilmSection = m.FilmSection,
                    Id = m.Id,
                    LengthMinutes = m.LengthMinutes,
                    PosterImage = m.PosterImage,
                    Quality = m.Quality,
                    Revenue = m.Revenue,
                    DirectorId = m.Director.Id
                }).ToList(),

                Series = director.Series.Select(s => new SeriesDirectorDTO
                {
                    Id = s.Id,
                    CreatedYear = s.CreatedYear,
                    Title=s.Title,
                    Description = s.Description,
                    Revenue = s.Revenue,
                    FilmSection = s.FilmSection,
                    LengthMinutes = s.LengthMinutes,
                    PosterImage = s.PosterImage,
                    Quality= s.Quality,
                    DirectorID = s.Director.Id
                   
                }).ToList()

            };
            return new GeneralResponse() { IsSuccess = true, Data = directorDTO }; 
        }


        [HttpGet]
        public ActionResult<dynamic> GetAllDirectors()
        {
            List<Director> directors = DirectorRepository.GetAllDirectors();
            List<DirectorDTO> directorDTOs = new List<DirectorDTO>();
            if (directors == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Directors"
                };
            }

            foreach(Director director in directors)
            {
                DirectorDTO directorDTO = new DirectorDTO
                {
                    Id = director.Id,
                    Name = director.Name,
                    Age = director.Age,
                    Image = director.Image,
                    Overview = director.Overview,
                    Movies = director.Movies.Select(m => new MoviesDirectorDTO
                    {
                        Title = m.Title,
                        CreatedYear = m.CreatedYear,
                        Description = m.Description,
                        FilmSection = m.FilmSection,
                        Id = m.Id,
                        LengthMinutes = m.LengthMinutes,
                        PosterImage = m.PosterImage,
                        Quality = m.Quality,
                        Revenue = m.Revenue,
                        DirectorId = m.Director.Id
                    }).ToList(),

                    Series = director.Series.Select(s => new SeriesDirectorDTO
                    {
                        Id = s.Id,
                        CreatedYear = s.CreatedYear,
                        Title = s.Title,
                        Description = s.Description,
                        Revenue = s.Revenue,
                        FilmSection = s.FilmSection,
                        LengthMinutes = s.LengthMinutes,
                        PosterImage = s.PosterImage,
                        Quality = s.Quality,
                        DirectorID = s.Director.Id

                    }).ToList()

                };
                directorDTOs.Add(directorDTO);
            }
           
            return new GeneralResponse() { IsSuccess = true, Data = directorDTOs };
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<dynamic> SearchDirector(string name)
        {
            List<Director> directors = DirectorRepository.SearchDirector(name);
            List<DirectorDTO> directorDTOs = new List<DirectorDTO>();
            if (directors == null || directors.Count == 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Directors"
                };
            }

            foreach (Director director in directors)
            {
                DirectorDTO directorDTO = new DirectorDTO
                {
                    Id = director.Id,
                    Name = director.Name,
                    Age = director.Age,
                    Image = director.Image,
                    Overview = director.Overview,
                    Movies = director.Movies.Select(m => new MoviesDirectorDTO
                    {
                        Title = m.Title,
                        CreatedYear = m.CreatedYear,
                        Description = m.Description,
                        FilmSection = m.FilmSection,
                        Id = m.Id,
                        LengthMinutes = m.LengthMinutes,
                        PosterImage = m.PosterImage,
                        Quality = m.Quality,
                        Revenue = m.Revenue,
                        DirectorId = m.Director.Id
                    }).ToList(),

                    Series = director.Series.Select(s => new SeriesDirectorDTO
                    {
                        Id = s.Id,
                        CreatedYear = s.CreatedYear,
                        Title = s.Title,
                        Description = s.Description,
                        Revenue = s.Revenue,
                        FilmSection = s.FilmSection,
                        LengthMinutes = s.LengthMinutes,
                        PosterImage = s.PosterImage,
                        Quality = s.Quality,
                        DirectorID = s.Director.Id

                    }).ToList()

                };
                directorDTOs.Add(directorDTO);
            }

            return new GeneralResponse() { IsSuccess = true, Data = directorDTOs };
        }





        [HttpDelete]
        [Route("{id}")]
        //ask messi
        public ActionResult<dynamic> DeleteDirector(int id)
        {
           
            Director  director = DirectorRepository.GetById(id);
            if (director != null)
            {
                director.IsDeleted = true;
                DirectorRepository.Delete(id);
                DirectorRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = director };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found  director , Please Enter Valid ID "
                };
            }

        }











    }
}
