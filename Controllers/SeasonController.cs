using Microsoft.AspNetCore.Http;
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

        //[HttpPut("{id:int}")]
        //public ActionResult<dynamic> EditSeason(int Id,SeasonsDTO seasonDTO)
        //{
        //    Season season = seasonsRepo.GetById(Id);
        //    if(season == null)
        //    {
        //        return new GeneralResponse()
        //        {
        //            IsSuccess = false,
        //            Data = "Season Not Found...!"
        //        };
        //    }
        //    else
        //    {

        //       season.Name = seasonDTO.Name;
        //       season.NumOfEpisodes= seasonDTO.NumOfEpisodes;
        //       season.SeriesID= seasonDTO.SeriesID;
        //        seasonsRepo.Update(season);
        //        seasonsRepo.Save();
        //        return new GeneralResponse() { IsSuccess = true, Data = seasonDTO };

        //    }
        //}



        [HttpPut("{id:int}")]
           public ActionResult<dynamic> edit(int id , SeasonsDTO seasonsDTO) {
            if (seasonsDTO == null)
            { return new GeneralResponse() { IsSuccess=false, Data="Not found" }; }
            else
            {
                Season season = new Season() {
                    Name = seasonsDTO.Name,
                    NumOfEpisodes = seasonsDTO.NumOfEpisodes,
                    SeriesID = seasonsDTO.SeriesID,
                };

                seasonsRepo.Update(season);
                seasonsRepo.Save();
                return new GeneralResponse() { 
                    Data = seasonsDTO,
                    IsSuccess = true    
                };
            }
        
        }


        [HttpDelete("{id:int}")]
        public ActionResult<dynamic> DeleteSeason(int Id) 
        { 
            Season season=seasonsRepo.GetById(Id);
            if (season == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Season Not Found...!"
                };
            }
            else
            {
                season.IsDeleted = true;
                seasonsRepo.Delete(Id);
                seasonsRepo.Save();
                return new GeneralResponse() { IsSuccess = true, Data = season };
            }
        }

    }
}
