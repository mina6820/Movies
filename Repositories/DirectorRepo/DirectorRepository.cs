using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.DirectorRepo
{
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        private readonly Context context;
        public DirectorRepository(Context _context) : base(_context)
        {
            context = _context;
        }
        public Director GetDirectorById(int id)
        {
            return context.Directors
                .Include(d => d.Movies)
                .Include(d=>d.Series)
                .FirstOrDefault(d => d.Id == id);
        }

        public List<Director> GetAllDirectors()
        {
            return context.Directors
               .Include(d => d.Movies)
               .Include(d => d.Series)
               .ToList();
        }

        public List<Director> SearchDirector(string name)
        {
            string searchNameLower = name.ToLower();

            List<Director> directors = context.Directors
                .Where(d => d.Name.ToLower().Contains(searchNameLower))
                .Include(d=>d.Movies)
                .Include(d=>d.Series)
                .ToList();
            return directors;
            
        }

    }
}
