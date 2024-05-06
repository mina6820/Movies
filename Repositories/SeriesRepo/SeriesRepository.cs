using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.SeriesRepo
{
    public class SeriesRepository : Repository<Series>, ISeriesRepository
    {
        private readonly Context context;

        public SeriesRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<Series> GetAllSeriesOfDirector(int directorId)
        { 
          return context.Series.Where(e=>e.DirectorID== directorId).ToList();
            
        }
        public List<Series> Search(string DirectorName)
        {
            return context.Series.Include(c=>c.Director).Where(c=>c.Director.Name.Contains(DirectorName)).ToList(); 
        }

        public Series GetSeries(int SeriesId)
        {
            return context.Series.Include(s=>s.Seasons).Include(s=>s.Director).FirstOrDefault(s => s.Id == SeriesId);
        }
    }
}
