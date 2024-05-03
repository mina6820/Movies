using Instagram_Clone.Repositories;
using Movies.Models;

namespace Movies.Repositories.SeasonsRepo
{
    public class SeasonsRepo : Repository<Season>, ISeasonsRepo
    {
        private readonly Context _context;
         public SeasonsRepo(Context context) : base(context)
        {
            _context = context;
        }
    }
}
