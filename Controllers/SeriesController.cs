﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.DirectorDTOs;
using Movies.Models;
using Movies.Repositories.DirectorRepo;
using Movies.Repositories.SeasonsRepo;
using Movies.Repositories.SeriesRepo;
using System.Diagnostics.Eventing.Reader;
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
        [HttpGet]
        public ActionResult<dynamic> GetAll()
        {
            List<Series> series=_seriesRepository.GetAll();
            if(series == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no data"
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = series
                };
            }

        }
        [HttpGet("{id:int}")]
        public ActionResult<dynamic> GetSeriesById(int id)
        {
            Series series= _seriesRepository.GetById(id);
            if(series == null) {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no data"
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = series
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

                _seriesRepository.Insert(series);
                _seriesRepository.Save();

                // Retrieve director information
                Director director = directorRepository.GetById(seriesDTO.DirectorID);
                DirectorDTO directorDTO = new DirectorDTO
                {
                    Id = director.Id,
                    Name = director.Name
                    // Populate other properties as needed
                };

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        Series = seriesDTO,
                        Director = directorDTO
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


    }
}
