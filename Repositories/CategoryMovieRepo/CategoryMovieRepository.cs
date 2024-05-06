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

        public bool DeleteMovieFromCategory(int CategoryId, int MovieId)
        {
            CategoryMovie categoryMovie = context.CategorieMovies.FirstOrDefault(cs => cs.CategoryID == CategoryId && cs.MovieID == MovieId);
            if (categoryMovie != null)
            {
                context.CategorieMovies.Remove(categoryMovie);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsMovieFoundInCategory(int MovieId, int CategoryId)
        {
            CategoryMovie categoryMovie = context.CategorieMovies.FirstOrDefault(cs => cs.CategoryID == CategoryId && cs.MovieID == MovieId);
            if (categoryMovie == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }




    }
}
