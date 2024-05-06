using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
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
                    Data = " Invalid Data (: "
                };
            }
        }


        [HttpDelete("{ActorId:int}/{SeriesId:int}")]
        public ActionResult<dynamic> DeleteActorFromSeries(int ActorId , int SeriesId)
        {
            bool IsDeleted = actorSeriesRepository.DeleteActorFromSeries(ActorId,SeriesId);
            if(IsDeleted)
            {
                return new GeneralResponse()
                {
                    IsSuccess = true,
                    Data = " Deleted Successfully "
                };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = " Invalid Data (: "
                };
            }
        }



        [HttpPut("{ActorId:int}/{SeriesId:int}")]
        public ActionResult<dynamic> EditActorsInSeries(int ActorId , int SeriesId , ActorSeriesDTO actorSeriesDTO)
        {
            bool IsFoundInDB = actorSeriesRepository.GetActorAndSeries(ActorId ,SeriesId);
            if (IsFoundInDB)
            {
                bool isActorInSeries = actorSeriesRepository.IsActorInSeries(ActorId, SeriesId);

                if (isActorInSeries)
                {
                    bool IsDTOFound = actorSeriesRepository.IsActorInSeries(actorSeriesDTO.ActorID, actorSeriesDTO.SeriesID);
                    if(!IsDTOFound)
                    {
                        ActorSeries actorSeries = actorSeriesRepository.GetActorSeries(ActorId, SeriesId);

                        actorSeries.SeriesID = actorSeriesDTO.SeriesID;
                        actorSeries.ActorID = actorSeriesDTO.ActorID;
                        actorSeriesRepository.Update(actorSeries);
                        actorSeriesRepository.Save();
                        return new GeneralResponse()
                        {
                            IsSuccess = true,
                            Data = actorSeries
                        };
                    }
                    else
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = "Actor Already Exist In This Sereis"
                        };
                    }
                   
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = "Actor Not Exist"
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
