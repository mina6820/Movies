using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.DirectorDTOs;
using Movies.Models;
using Movies.Repositories.DirectorRepo;
using Movies.Repositories.SeasonsRepo;
using Movies.Repositories.SeriesRepo;
using System.Diagnostics.Eventing.Reader;
using System.IO;
namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly ISeasonsRepo _seasonsRepo;
        private readonly IDirectorRepository directorRepository;

        public SeriesController(ISeriesRepository seriesRepository,ISeasonsRepo seasonsRepo , IDirectorRepository directorRepository)
        {
            _seriesRepository = seriesRepository;
            _seasonsRepo = seasonsRepo;
            this.directorRepository = directorRepository;
        }
        //[HttpGet]
        //public ActionResult<dynamic> GetAll()
        //{
        //    List<Series> series=_seriesRepository.GetAll();
        //    if(series == null)
        //    {
        //        return new GeneralResponse()
        //        {
        //            IsSuccess = false,
        //            Data = "There is no data"
        //        };
        //    }
        //    else
        //    {
        //        return new GeneralResponse()
        //        {
        //            IsSuccess = true,
        //            Data = series
        //        };
        //    }

        //}
        [HttpGet("{id:int}")]
        public ActionResult<dynamic> GetSeriesById(int id)
        {
            Series series = _seriesRepository.GetSeries(id);
            if (series == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no data"
                };
            }
            else
            {
                SeriesToGetDTO seriesToGetDTO = new SeriesToGetDTO()
                {

                    SeriesId = series.Id,
                    CreatedYear = series.CreatedYear,
                    Description = series.Description,
                    DirectorID = series.DirectorID,
                    FilmSection = series.FilmSection,
                    LengthMinutes = series.LengthMinutes,
                    PosterImage = series.PosterImage,
                    Quality = series.Quality,
                    Revenue = series.Revenue,
                    Title = series.Title,
                    DirectorName = series.Director.Name,
                    Seasons = series.Seasons.Select(season => new SeasonsDTO
                    {
                        NumOfEpisodes = season.NumOfEpisodes,
                        Name = season.Name,
                        SeriesID = season.SeriesID // Assuming you want to include the SeriesID in each SeasonDTO
                    }).ToList(),

                };
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = seriesToGetDTO
                };
            }
        }
        [HttpPost]
        public ActionResult<dynamic> AddSeries(SeriesDTO seriesDTO)
        {
            if (ModelState.IsValid)
            {
                Series series = new Series()
                {
                    Title = seriesDTO.Title,
                    CreatedYear = seriesDTO.CreatedYear,
                    Description = seriesDTO.Description,
                    PosterImage = seriesDTO.PosterImage,
                    DirectorID = seriesDTO.DirectorID,
                    Revenue = seriesDTO.Revenue,
                    LengthMinutes = seriesDTO.LengthMinutes,
                };

                Director director = directorRepository.GetById(seriesDTO.DirectorID);
                if (director == null)
                {
                    return new GeneralResponse() { IsSuccess = true, Data = "Director Not Found" };
                }


                _seriesRepository.Insert(series);
                _seriesRepository.Save();


                // Retrieve director information
              
                DirectorInSeriesDTO directorInSeriesDTO  = new DirectorInSeriesDTO
                {
                    Id = director.Id,
                    DirectorName = director.Name
                    // Populate other properties as needed
                };

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        Series = seriesDTO,
                        Director = directorInSeriesDTO
                    }
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Added Failed"
                };
            }
        }
        [HttpDelete("{id:int}")]
        public ActionResult<dynamic> DeleteSeries(int id)
        {
            Series series = _seriesRepository.GetById(id);
            if (series == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Series Not Found"
                };
            }
            else
            {
                series.IsDeleted = true;
                _seriesRepository.Update(series);
                _seriesRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = "Series Deleted Successfully" };
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<dynamic> EditSeries(int id, SeriesDTO seriesDTO)
        {
            Series series = _seriesRepository.GetById(id);
            if (series == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Series Not Found"
                };
            }
            else
            {
                series.Title = seriesDTO.Title;
                series.CreatedYear = seriesDTO.CreatedYear;
                series.Description = seriesDTO.Description;
                series.PosterImage = seriesDTO.PosterImage;
                series.DirectorID = seriesDTO.DirectorID;
                series.Revenue = seriesDTO.Revenue;
                series.LengthMinutes = seriesDTO.LengthMinutes;

                _seriesRepository.Update(series);
                _seriesRepository.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = "Edit Successfully"
                };
            }
        }

    }
}
