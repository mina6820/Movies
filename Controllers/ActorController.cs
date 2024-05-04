using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs.ActorDTOs;
using Movies.Models;
using Movies.Repositories.ActroRepo;
using System.IO;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        IActorRepository ActorRepository;
        public ActorController(IActorRepository _ActorRepository)
        {
            ActorRepository = _ActorRepository;
        }

        [HttpPost]
        public ActionResult<dynamic> AddActor(ActorDTO actorDTO)
        {
            if (ModelState.IsValid)
            {
                Actor actor = new Actor()
                {
                    Age = actorDTO.Age,
                    ID = actorDTO.ID,
                    Image = actorDTO.Image,
                    Name = actorDTO.Name,
                    Overview = actorDTO.Overview
                };
                ActorRepository.Insert(actor);
                ActorRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = actor };
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

        [HttpGet]
        [Authorize]
        public ActionResult<dynamic> GetAllActors()
        {
            List<Actor> actors = ActorRepository.GetAll();
            List<ActorDTO> actorDTOs = new List<ActorDTO>();

            if (actors !=null)
            {
                foreach (Actor actor in actors)
                {
                    ActorDTO actorDTO = new ActorDTO()
                    {
                        Name = actor.Name,
                        ID = actor.ID,
                        Image = actor.Image,
                        Overview = actor.Overview,
                        Age = actor.Age

                    };
                    actorDTOs.Add(actorDTO);
                }
                return new GeneralResponse() { IsSuccess = true, Data = actorDTOs };
            }

            else
            {
                return new GeneralResponse()
                { 
                    IsSuccess = false,
                    Data = "Not Found Actors "
                };
            }
               
        
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<dynamic> GetActorById(int id)
        {
            Actor actor = ActorRepository.GetById(id);
           
            if (actor == null)
            {
                return new GeneralResponse() { IsSuccess = false,
                    Data = "Not Found Actor , Please Enter Valid ID " };

            }
            else
            {
                ActorDTO actorDTO = new ActorDTO()
                {
                    ID = actor.ID,
                    Name = actor.Name,
                    Age = actor.Age,
                    Image = actor.Image,
                    Overview = actor.Overview
                };

                return new GeneralResponse() { IsSuccess = true, Data = actorDTO };
            }
        }


        [HttpGet("{name:alpha}")]
        public ActionResult<dynamic> SearchActor(string name)
        {
            List<Actor> actors = ActorRepository.SearchActor(name);
            List<ActorDTO> actorDTOs = new List<ActorDTO>();

            if (actors == null || actors.Count == 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Actors "
                };
            }
            else
            {
                foreach (Actor actor in actors)
                {
                    ActorDTO actorDTO = new ActorDTO()
                    {
                        Name = actor.Name,
                        ID = actor.ID,
                        Image = actor.Image,
                        Overview = actor.Overview,
                        Age = actor.Age

                    };
                    actorDTOs.Add(actorDTO);
                    
                }
                return new GeneralResponse() { IsSuccess = true, Data = actorDTOs };
            }

        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult<dynamic> DeleteActor(int id)
        {
            //ActorRepository.DeleteActor(id);
            Actor actor  =  ActorRepository.GetById(id);
            if (actor != null)
            {
                actor.IsDeleted = true;
                ActorRepository.Delete(id);
                ActorRepository.Save();
                return new GeneralResponse() { IsSuccess = true, Data = actor };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Actor , Please Enter Valid ID "
                };
            }
           
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<dynamic> EditActor(int id,ActorDTO actorDTO)
        {
            Actor ReturnedActor = ActorRepository.GetById(id);
            if (ReturnedActor == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = "Not Found Actor , Please Enter Valid ID "
                };
            }
            else
            {
                ReturnedActor.ID = actorDTO.ID;
                ReturnedActor.Name = actorDTO.Name;
                ReturnedActor.Image = actorDTO.Image;
                ReturnedActor.Overview = actorDTO.Overview;
                ActorRepository.Update(ReturnedActor);
                ActorRepository.Save();

                return new GeneralResponse() { IsSuccess = true, Data = actorDTO };
            }
        }
    }
}
