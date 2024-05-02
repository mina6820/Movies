using Microsoft.EntityFrameworkCore;
using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.DirectorRepo
{
    public interface IDirectorRepository : IRepository<Director>
    {
        public Director GetDirectorById(int id);
        public List<Director> GetAllDirectors();

        public List<Director> SearchDirector(string name);
    }
}
