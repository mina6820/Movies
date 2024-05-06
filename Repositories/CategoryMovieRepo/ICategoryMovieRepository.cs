using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.CategoryMovieRepo
{
    public interface ICategoryMovieRepository : IRepository<CategoryMovie>
    {
        public List<Movie> GetMoviesByCategoryId(int categoryId);
        public bool DeleteMovieFromCategory(int CategoryId, int MovieId);

        public bool IsMovieFoundInCategory(int MovieId, int CategoryId);

    }
}
