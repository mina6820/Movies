using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Repositories.ActorSeriesRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorSeriesController : ControllerBase
    {
        private readonly IActorSeriesRepository actorSeriesRepository;
        public ActorSeriesController(IActorSeriesRepository actorSeriesRepository) {
            
            this.actorSeriesRepository = actorSeriesRepository;
        }

        [HttpPost("{ActorId:int}/{SeriesId:int}")]
        public ActionResult<dynamic> AddActorToSeries(int ActorId , int SeriesId) 
        { 
            bool isFound=actorSeriesRepository.GetActorAndSeries(ActorId, SeriesId);
            if (isFound)
            {
                bool IsActorInSeries=actorSeriesRepository.IsActorInSeries(ActorId, SeriesId);
                if (!IsActorInSeries)
                {
                    ActorSeries actorSeries = new ActorSeries()
                    {
                        ActorID = ActorId,
                        SeriesID = SeriesId
                    };

                    actorSeriesRepository.Insert(actorSeries);
                    actorSeriesRepository.Save();

                    return new GeneralResponse()
                    {
                        IsSuccess = true,
                        Data = "Added Successfully"
                    };
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = " this Actor Found in this Series (: "
                    };
                }
                
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = " invalid Data (: "
                };
            }
        }
    }
}
