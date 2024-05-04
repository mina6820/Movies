using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.CategoryMovieRepo
{
    public class CategoryMovieRepository : Repository<CategoryMovie>, ICategoryMovieRepository
    {
        private readonly Context context;

        public CategoryMovieRepository(Context context) : base(context)
        {
            this.context = context;
        }
        public List<Movie> GetMoviesByCategoryId(int categoryId)
        {
            return context.CategorieMovies
                    .Include(cm => cm.Movie)
                    .ThenInclude(m => m.Director)
                    .Where(cm => cm.CategoryID == categoryId && cm.Movie.IsDeleted == false)
                    .Select(cm => cm.Movie)
                    .ToList();
        }


    }
}
