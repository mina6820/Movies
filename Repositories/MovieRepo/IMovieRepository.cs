using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.MovieRepo
{
    public interface IMovieRepository : IRepository<Movie>
    {
        public Movie GetMovieById(int id);
        public List<Movie> GetMovieByName(string name);
    }
}
