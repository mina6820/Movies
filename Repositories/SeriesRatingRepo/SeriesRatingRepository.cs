using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.SeriesRatingRepo
{
    public class SeriesRatingRepository: Repository<SeriesRating>, ISeriesRatingRepository
    {
        private readonly Context _context;
        public SeriesRatingRepository(Context _context) : base(_context)
        {
            this._context = _context;
        }

        public double GetSeriesRating(int SeriesID)
        {
            List<int> SeriesRatings =
                    _context
                    .SeriesRatings
                    .Where(sr => sr.SeriesID == SeriesID && sr.IsDeleted == false)
                    .Select(sr => sr.Rate)
                    .ToList();

            if (SeriesRatings.Count == 0)
            {
                return 0;
            }

            double SeriesAVGRate = SeriesRatings.Sum() / (double)SeriesRatings.Count;
            return SeriesAVGRate;
        }
    }
}
