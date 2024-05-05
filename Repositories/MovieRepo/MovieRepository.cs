using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.DTOs;
using Movies.Models;

namespace Movies.Repositories.MovieRepo
{
    public class MovieRepository : Repository<Movie> , IMovieRepository
    {
        private readonly Context context;

        public MovieRepository(Context context) : base(context)
        {
            this.context = context;
        }
        
        public List<Movie> GetAll()
        {
            return context.Movies.Where(m => m.IsDeleted== false).Include(m => m.Director).ToList();
        }

        public Movie GetMovieById(int id)
        {
            return context.Movies.Include(m => m.Director).FirstOrDefault(m => m.Id == id && m.IsDeleted == false); 
        }

        public List<Movie> GetMovieByName(string name)
        {
            return context.Movies.Include(m => m.Director).Where(m => m.Title.Contains(name) && m.IsDeleted == false).ToList();
        }

    }
}
