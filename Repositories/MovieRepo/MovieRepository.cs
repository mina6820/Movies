using Instagram_Clone.Repositories;
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
        

    }
}
