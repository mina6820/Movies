using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.CategoryMovieRepo
{
    public interface ICategoryMovieRepository : IRepository<CategoryMovie>
    {
        public List<Movie> GetMoviesByCategoryId(int categoryId);
    }
}
