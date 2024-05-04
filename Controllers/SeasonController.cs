using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
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

        [HttpPost]
        public ActionResult<dynamic> addSeason(SeasonsDTO seasonsDTO)
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
    }
}
