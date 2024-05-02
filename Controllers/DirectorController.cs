using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.ActroRepo;
namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DirectorController : ControllerBase
    {
        IActorRepository ActorRepository;
        public DirectorController(IActorRepository _ActorRepository)
        {
            ActorRepository = _ActorRepository;
        }
    }
}
