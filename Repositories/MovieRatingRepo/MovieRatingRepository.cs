using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.MovieRatingRepo
{
    public class MovieRatingRepository : Repository<MovieRating>, IMovieRatingRepository
    {
        private readonly Context _context;
        public MovieRatingRepository(Context _context) : base(_context)
        {
            this._context = _context;
        }

        public double GetMovieRating(int MovieID)
        {
            List<int> MovieRatings = 
                _context
                .movieRatings
                .Where(mr => mr.MovieID == MovieID && mr.IsDeleted == false)
                .Select(mr => mr.Rate)
                .ToList();
            if (MovieRatings.Count == 0)
            {
                return 0;
            }

            double MovieAVGRate = MovieRatings.Sum() / (double)MovieRatings.Count;
            return MovieAVGRate;
        }
    }
}
