using Instagram_Clone.Repositories;
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
    }
}
