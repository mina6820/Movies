using Instagram_Clone.Repositories;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.SeasonsRepo
{
    public class SeasonsRepo : Repository<Season>, ISeasonsRepo
    {
        private readonly Context context;
         public SeasonsRepo(Context context) : base(context)
        {
            this.context = context;
        }

       public List<Season> GetAllSeasonsWithSeries()
        {
            return context.Seasons.Include(s=>s.Series).ToList();
        }

       public Season GetSeasonWithSeries(int id)
        {
            return context.Seasons.Include(s => s.Series).FirstOrDefault(s => s.Id == id); ;
        }
    }
}
