using Instagram_Clone.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Repositories.SeriesCategoryRepo
{
    public class SeriesCategoryRepository : Repository<CategorySeries>, ISeriesCategoryRepository
    {

        private readonly Context context;
        public SeriesCategoryRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<Series> GetAllSeriesInCategory(int categoryId)
        {
            List<CategorySeries> categorySeries = context.CategorieSeries.Include(s => s.Series).Include(s=>s.Series.Director).Include(s=>s.Series.Seasons).Where(s => s.CategoryID == categoryId).ToList();
            List<Series> series = new List<Series>();

            foreach (var category in categorySeries)
            {
                Series seriesItem = category.Series;
                series.Add(seriesItem);

            }

            return series;

        }

        public bool GetCategoriesAndSeries(int CategoryId, int SeriesId)
        {
            Category category  = context.Categories.FirstOrDefault(c => c.Id == CategoryId);
            Series series = context.Series.FirstOrDefault(s => s.Id == SeriesId);

            if (category != null && series != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CategorySeries GetCategorySeries(int CategoryId,int SeriesId) 
        {
            return context.CategorieSeries.FirstOrDefault(cs => cs.CategoryID == CategoryId && cs.SeriesID == SeriesId);
        }


        public bool IsCategoryFound(int CategoryId)
        {
            CategorySeries categorySeries = context.CategorieSeries.FirstOrDefault(cs => cs.CategoryID == CategoryId);

            if (categorySeries == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool IsSeriesFoundInCategory(int SeriesId, int CategoryId)
        {
            CategorySeries categorySeries = context.CategorieSeries.FirstOrDefault(cs => cs.CategoryID == CategoryId && cs.SeriesID == SeriesId);
            if (categorySeries == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool DeleteCategorySeries(int CategorySeriesId)
        {
            CategorySeries categorySeries = context.CategorieSeries.FirstOrDefault(cs => cs.Id == CategorySeriesId);
            if (categorySeries != null)
            {
                context.CategorieSeries.Remove(categorySeries);
                context.SaveChanges(); 
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteSeriesFromCategory(int CategoryId , int SeriesId)
        {
            CategorySeries categorySeries = context.CategorieSeries.FirstOrDefault(cs => cs.CategoryID == CategoryId && cs.SeriesID== SeriesId);
            if (categorySeries != null)
            {
                context.CategorieSeries.Remove(categorySeries);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}
