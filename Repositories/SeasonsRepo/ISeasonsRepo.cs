using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.SeasonsRepo
{
    public interface ISeasonsRepo :IRepository<Season>
    {
        public List<Season> GetAllSeasonsWithSeries();
        public Season GetSeasonWithSeries(int id);
    }
}
