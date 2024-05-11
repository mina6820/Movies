using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;

using Movies.DTOs.DirectorDTOs;
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
        public ActionResult<dynamic> AddDirector(DirectorDTOForAddandEdit directorDTOForAddandEdit)
        {
            if (ModelState.IsValid)
            {
                Director director  = new Director()
                {
                    Age = directorDTOForAddandEdit.Age,
                    //Id = directorDTOForAddandEdit.Id,
                    Image = directorDTOForAddandEdit.Image,
                    Name = directorDTOForAddandEdit.Name,
                    Overview = directorDTOForAddandEdit.Overview,
                    
                };

                //director.Movies = directorDTO.Movies.Select(m => new Movie()
                //{
                //    Title = m.Title,
                //    Id= m.Id
                    
                   
                //}).ToList();

                //director.Series = directorDTO.Series.Select(s => new Series()
                //{
                //    Title = s.Title,
                //    Id = s.Id

                //}).ToList();


                DirectorRepository.Insert(director);
                DirectorRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = directorDTOForAddandEdit };
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
                    Id = m.Id,
                    PosterImage=m.PosterImage
                }).ToList(),

                Series = director.Series.Select(s => new SeriesDirectorDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    PosterImage = s.PosterImage
                }).ToList()

            };
            return new GeneralResponse() { IsSuccess = true, Data = directorDTO }; 
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<dynamic> EditDirector(int id, DirectorDTOForAddandEdit directorDTOForAddandEdit)
        {
            Director returnedDirector = DirectorRepository.GetDirectorById(id);
            if (returnedDirector == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Director, Please Enter Valid ID"
                };
            }
            else
            {
                returnedDirector.Name = directorDTOForAddandEdit.Name;
                returnedDirector.Age = directorDTOForAddandEdit.Age;
                returnedDirector.Image = directorDTOForAddandEdit.Image;
                returnedDirector.Overview = directorDTOForAddandEdit.Overview;

                // Update the movies and series associated with the director
                //returnedDirector.Movies = directorDTO.Movies.Select(m => new Movie()
                //{
                //    Title = m.Title,
                //    Id = m.Id 
                //}).ToList();

                //returnedDirector.Series = directorDTO.Series.Select(s => new Series()
                //{
                //    Title = s.Title,
                //    Id = s.Id
                //}).ToList();

                DirectorRepository.Update(returnedDirector);
                DirectorRepository.Save();

                return new GeneralResponse() { IsSuccess = true, Data = directorDTOForAddandEdit };
            }
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
                        Id = m.Id,
                        PosterImage = m.PosterImage


                    }).ToList(),

                    Series = director.Series.Select(s => new SeriesDirectorDTO
                    {
                        Id = s.Id,
                        Title = s.Title,
                        PosterImage = s.PosterImage
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
                    //Movies = director.Movies.Select(m => new MoviesDirectorDTO
                    //{
                    //    Title = m.Title,
                    //    Id = m.Id
                    //}).ToList(),

                    //Series = director.Series.Select(s => new SeriesDirectorDTO
                    //{
                    //    Id = s.Id,
                      
                    //    Title = s.Title
                    //}).ToList()

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
