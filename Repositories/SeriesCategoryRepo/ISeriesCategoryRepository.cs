using Movies.Models;
using TestingMVC.Repo;

namespace Movies.Repositories.SeriesCategoryRepo
{
    public interface ISeriesCategoryRepository :IRepository<CategorySeries> 
    {
        public List<Series> GetAllSeriesInCategory(int categoryId);
        public bool IsCategoryFound(int CategoryId);
        public bool IsSeriesFoundInCategory(int SeriesId , int CategoryId);
        public CategorySeries GetCategorySeries(int CategoryId, int SeriesId);
        public bool DeleteCategorySeries(int CategorySeriesId);
        public bool DeleteSeriesFromCategory(int CategoryId, int SeriesId);
        public bool GetCategoriesAndSeries(int CategoryId, int SeriesId);

    }
}
