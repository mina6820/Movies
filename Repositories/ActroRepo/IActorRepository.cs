using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.ActroRepo
{
    public interface IActorRepository :IRepository<Actor>
    {
        //public Actor GetActorById(int id);
        //public void DeleteActor(int id);
        public List<Actor> SearchActor(string name);
    }
}
