using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.CategoryRepo
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly Context context;

        public CategoryRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public Category? GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id && c.IsDeleted == false);
        }

        public List<Category> GetAll()
        {
            return context.Categories.Where(c => c.IsDeleted == false).ToList();
        }

        public List<Category>? GetCategoryByName(string name)
        {
            return context.Categories.Where(c => c.Name.Contains(name) && c.IsDeleted == false).ToList();
        }


    }
}
