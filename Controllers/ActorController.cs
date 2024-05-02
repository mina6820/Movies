using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.ActroRepo;

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
        public ActionResult<dynamic> AddActor(Actor actor)
        {
            ActorRepository.Insert(actor);
            ActorRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = actor };
            //return CreatedAtAction(nameof(GetActorById), new { id = actor.ID }, actor);
        }


        [HttpGet]
        public IActionResult GetAllActors()
        {
            List<Actor> actors = ActorRepository.GetAll();
            return Ok(actors);
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
        public IActionResult GetActorByName(string name)
        {
            Actor actor = ActorRepository.GetActorByName(name);
            return Ok(actor);
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteActor(int id)
        {
            //ActorRepository.DeleteActor(id);
            Actor actor  =  ActorRepository.GetById(id);
            if (actor != null)
            {
                actor.IsDeleted = true;
                ActorRepository.Delete(id);
                ActorRepository.Save();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
           
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult EditActor(int id,Actor actor)
        {
            Actor ReturnedActor = ActorRepository.GetById(id);
            ReturnedActor.ID = actor.ID;
            ReturnedActor.Name = actor.Name;
            ReturnedActor.Image = actor.Image;
            ReturnedActor.Overview = actor.Overview;
            ActorRepository.Update(ReturnedActor);
            ActorRepository.Save();
            return Ok(ReturnedActor);
        }
    }
}
