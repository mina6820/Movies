using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.ActroRepo
{
    public class ActorRepository : Repository<Actor> , IActorRepository
    {
        private readonly Context context;
        public ActorRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<Actor> SearchActor(string name)
        {
            string searchNameLower = name.ToLower();

            List<Actor> actors = context.Actors.Where(a => a.Name.ToLower().Contains(searchNameLower)).ToList();
            return actors;
        }


    }
}
