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

        //public Actor GetActorById(int id)
        //{
        //    return context.Actors.Where(a=>a.IsDeleted==false).FirstOrDefault(a => a.ID == id);
        //}

        //public void DeleteActor(int id)
        //{
        //    Actor actor = GetActorById(id);
        //    actor.IsDeleted = true;
        //    context.SaveChanges();

        //}
        public Actor GetActorByName(string name)
        {

            Actor actor = context.Actors.Where(a=>a.IsDeleted==false).FirstOrDefault(a => a.Name == name);
            return actor;
        }

    }
}
