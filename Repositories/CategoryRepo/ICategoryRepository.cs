using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.CategoryRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {

        public Category GetCategoryById(int id);

        public List<Category>? GetCategoryByName(string name);
    }
}
