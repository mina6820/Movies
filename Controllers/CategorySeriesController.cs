using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.SeriesCategoryRepo;
using Movies.Repositories.SeriesRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySeriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISeriesRepository seriesRepository;
        private readonly ISeriesCategoryRepository seriesCategoryRepository;

        public CategorySeriesController(ICategoryRepository categoryRepository, ISeriesRepository seriesRepository , ISeriesCategoryRepository seriesCategoryRepository) 
        {
            this.categoryRepository = categoryRepository;
            this.seriesRepository = seriesRepository;
            this.seriesCategoryRepository = seriesCategoryRepository;
        }

        [HttpPost("{SeriesId:int}/{CategoryId:int}")]

        public ActionResult<dynamic> AddSeriesToCategory(int SeriesId , int CategoryId )
        {
           Category category = categoryRepository.GetCategoryById(CategoryId);
            Series series = seriesRepository.GetById(SeriesId);

            if (category != null && series != null )
            {
                CategorySeries categorySeries = new CategorySeries()
                {
                    CategoryID = CategoryId,
                    SeriesID = SeriesId

                };

                seriesCategoryRepository.Insert(categorySeries);
                seriesCategoryRepository.Save();

                return new GeneralResponse() { IsSuccess = false, Data = categorySeries };


            }
            else
            {
                return new GeneralResponse() { IsSuccess = true, Data = "Added Failed " };
            }
        }
        [HttpGet]
        [Route("{CategoryId}")]

        public ActionResult<dynamic> GetAllSeriesInCategory(int CategoryId)
        {

            Category category = categoryRepository.GetCategoryById(CategoryId);
            if(category ==null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Category" };
            }

            bool CategoryFound = seriesCategoryRepository.IsCategoryFound(CategoryId);

            if (CategoryFound)
            {
                List<Series> series = seriesCategoryRepository.GetAllSeriesInCategory(CategoryId);
                return new GeneralResponse() { IsSuccess = true, Data = series };
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Category is not Exist" };
            }
          
        }

        [HttpDelete]
        [Route("{CategorySeriesId}")]
        public ActionResult<dynamic> DeleteSeriesFromCategory(int CategorySeriesId)
        {
          CategorySeries categorySeries =  seriesCategoryRepository.GetById(CategorySeriesId);
            if (categorySeries == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid CategorySeries" };
            }

            bool IsDeleted = seriesCategoryRepository.DeleteCategorySeries(CategorySeriesId);
            if (IsDeleted)
            {
                return new GeneralResponse() { IsSuccess = true, Data = "Deleted Successfully" };
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Series Already Doesnot Exist In This Category" };
            }

        }


        [HttpPut("{CategorySeriesId:int}")]

        public ActionResult<dynamic> EditCategorySeries(int CategorySeriesId , CategorySeriesDTO categorySeriesDTO)
        {
            if (ModelState.IsValid)
            {
                CategorySeries categorySeries = seriesCategoryRepository.GetById(CategorySeriesId);
                if (categorySeries == null)
                {
                    return new GeneralResponse() { IsSuccess = false, Data = "Invalid Category Series" };
                }
                else
                {
                    categorySeries.SeriesID = categorySeriesDTO.SeriesId;
                    categorySeries.CategoryID = categorySeriesDTO.CategoryId;
                    seriesCategoryRepository.Update(categorySeries);
                    seriesCategoryRepository.Save();
                    return new GeneralResponse() { IsSuccess = true, Data = "Updated Successfully" };
                }
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = " Updated Failed " };
            }

        }


    }
}
