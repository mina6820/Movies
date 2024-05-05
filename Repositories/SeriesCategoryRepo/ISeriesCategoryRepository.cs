using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.SeriesCategoryRepo
{
    public interface ISeriesCategoryRepository :IRepository<CategorySeries> 
    {
        public List<Series> GetAllSeriesInCategory(int categoryId);
        public bool IsCategoryFound(int CategoryId);
        public bool IsSeriesFoundInCategory(int SeriesId , int CategoryId);

        public bool DeleteCategorySeries(int CategorySeriesId);

    }
}
