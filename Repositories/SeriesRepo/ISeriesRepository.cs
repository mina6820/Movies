using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.SeriesRepo
{
    public interface ISeriesRepository :IRepository<Series>
    {
        public List<Series> GetAllSeriesOfDirector(int directorId);
        public List<Series> Search(string DirectorName);
        public Series GetSeries(int SeriesId);
    }
}
