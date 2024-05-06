using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.DTOs.ActorDTOs;
using Movies.Models;
using Movies.Repositories.ActorSeriesRepo;
using Movies.Repositories.ActroRepo;
using Movies.Repositories.SeriesRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorSeriesController : ControllerBase
    {
        private readonly IActorSeriesRepository actorSeriesRepository;
        private readonly ISeriesRepository seriesRepository;
        private readonly IActorRepository actorRepository;

        public ActorSeriesController(IActorSeriesRepository actorSeriesRepository,ISeriesRepository seriesRepository,IActorRepository actorRepository) {
            
            this.actorSeriesRepository = actorSeriesRepository;
            this.seriesRepository = seriesRepository;
            this.actorRepository = actorRepository;

        }
        [HttpGet("{SeriesId:int}")]
        public ActionResult<dynamic> GetAllActorsInSeries(int SeriesId) {
            List<Actor> actors= actorSeriesRepository.GetActors(SeriesId);
            Series series= seriesRepository.GetSeries(SeriesId);
            if (series == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no series"
                };
            }
            if (actors.Count==0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no actor in this series"
                };
            }
            else
            {
                List<ActorDTO> actorDTOs = new List<ActorDTO>();
                foreach (Actor actor in actors)
                {
                    ActorDTO actorDTO = new ActorDTO()
                    {
                        Age = actor.Age,
                        ID = actor.ID,
                        Image = actor.Image,
                        Name = actor.Name,
                        Overview = actor.Overview
                    };
                    actorDTOs.Add(actorDTO);
                }
                return new GeneralResponse()
                {

                    IsSuccess = true,
                    Data = actorDTOs
                };
            }

        }
        [HttpGet("actor/{ActorID:int}")]
        public ActionResult<dynamic> GetAllSeriesOfActor(int ActorID)
        {
            List<Series> series = actorSeriesRepository.GetSeriesOfActor(ActorID);
            Actor actor=actorRepository.GetById(ActorID);
            if (actor == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no actor"
                };
            }
            if (series.Count == 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "There is no series for this actor"
                };
            }
            else
            {
                List<SeriesToGetDTO> seriesToGetDTOs=new List<SeriesToGetDTO>();
                foreach (var item in series)
                {
                    SeriesToGetDTO seriesToGetDTO = new SeriesToGetDTO()
                    {

                        SeriesId = item.Id,
                        CreatedYear = item.CreatedYear,
                        Description = item.Description,
                        DirectorID = item.DirectorID,
                        FilmSection = item.FilmSection,
                        LengthMinutes = item.LengthMinutes,
                        PosterImage = item.PosterImage,
                        Quality = item.Quality,
                        Revenue = item.Revenue,
                        Title = item.Title,
                        DirectorName = item.Director.Name,
                        Seasons = item.Seasons.Select(season => new SeasonsDTO
                        {
                            NumOfEpisodes = season.NumOfEpisodes,
                            Name = season.Name,
                            SeriesID = season.SeriesID // Assuming you want to include the SeriesID in each SeasonDTO
                        }).ToList(),

                    };

                    seriesToGetDTOs.Add(seriesToGetDTO);
                }
                return new GeneralResponse()
                {

                    IsSuccess = true,
                    Data = seriesToGetDTOs
                };
            }

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
