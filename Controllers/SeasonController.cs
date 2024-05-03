﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.ActorDTOs;
using Movies.Models;
using Movies.Repositories.ActroRepo;
using Movies.Repositories.SeasonsRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonsRepo seasonsRepo;

        public SeasonController( ISeasonsRepo seasonsRepo )
        {
            this.seasonsRepo = seasonsRepo;
        }
        [HttpGet]
        public ActionResult<dynamic> GetAll()
        {
            List<Season> seasons = seasonsRepo.GetAll();
            if (seasons == null)
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
                    Data = seasons
                };
            }
        }

        //[HttpGet("{id:int}")]
        //public ActionResult<dynamic> GetById(int Id) {
        //Season season=seasonsRepo.GetById(Id);

        //    if (season == null)
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
        //            Data = season
        //        };
        //    }
        //}

        [HttpGet("{id:int}")]
        public ActionResult<dynamic> GetSes(int id) {
        
            Season season = seasonsRepo.GetById(id);
            if (season == null)
            {
                return new GeneralResponse() { Data = "Not Found", IsSuccess = false };
            }else {

                return new GeneralResponse() { IsSuccess = true, Data = season };
            }
        }
        [HttpPost]
        public ActionResult<dynamic> AddSeason(SeasonsDTO seasonsDTO)
        {
            if (seasonsDTO == null)
            {
                return new GeneralResponse() { 
                    
                    Data="add field",
                    IsSuccess=false
                };
            }
            else
            {
                Season season = new Season() {
                    NumOfEpisodes=seasonsDTO.NumOfEpisodes,
                    SeriesID=seasonsDTO.SeriesID,
                    Name=seasonsDTO.Name,
                };
                seasonsRepo.Insert(season);
                seasonsRepo.Save();

                return new GeneralResponse()
                {

                    Data = seasonsDTO,
                    IsSuccess = true
                };
            }
        }

       



        [HttpPut("{id:int}")]
           public ActionResult<dynamic> edit(int id , SeasonsDTO seasonsDTO) {
            Season season=seasonsRepo.GetById(id);
            if (season == null)
            { return new GeneralResponse() { IsSuccess=false, Data="Not found" }; }
            else
            {
                season.Name = seasonsDTO.Name;
                season.NumOfEpisodes = seasonsDTO.NumOfEpisodes;
                season.SeriesID = seasonsDTO.SeriesID;

                seasonsRepo.Update(season);
                seasonsRepo.Save();

                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = season
                };
            }
        
        }


        

        [HttpDelete("{id:int}")]
        public ActionResult<dynamic> Delete(int id)
        {
            Season season = seasonsRepo.GetById(id);
            if (season == null)
            {
                return new GeneralResponse() { IsSuccess=false , Data="Season Not Found" };

            }
            else
            {
                season.IsDeleted = true;
                seasonsRepo.Update(season);
                seasonsRepo.Save();
                return new GeneralResponse() { IsSuccess = true, Data = "Season Deleted Successfully" };
                 
            }
        }

    }
}
