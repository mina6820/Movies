using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
using Movies.Models;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.DirectorRepo;
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
        private readonly IDirectorRepository directorRepository;
        public CategorySeriesController(ICategoryRepository categoryRepository, ISeriesRepository seriesRepository, ISeriesCategoryRepository seriesCategoryRepository, IDirectorRepository directorRepository)
        {
            this.categoryRepository = categoryRepository;
            this.seriesRepository = seriesRepository;
            this.seriesCategoryRepository = seriesCategoryRepository;
            this.directorRepository = directorRepository;

        }

        [HttpPost("{SeriesId:int}/{CategoryId:int}")]

        public ActionResult<dynamic> AddSeriesToCategory(int SeriesId, int CategoryId)
        {
            Category category = categoryRepository.GetCategoryById(CategoryId);
            Series series = seriesRepository.GetById(SeriesId);

            if (category != null && series != null)
            {
                bool IsSeriesFound = seriesCategoryRepository.IsSeriesFoundInCategory(SeriesId, CategoryId);
                if (IsSeriesFound)
                {
                    return new GeneralResponse() { IsSuccess = false, Data = "Series Already Exist" };
                }
                CategorySeries categorySeries = new CategorySeries()
                {
                    CategoryID = CategoryId,
                    SeriesID = SeriesId

                };

                seriesCategoryRepository.Insert(categorySeries);
                seriesCategoryRepository.Save();

                return new GeneralResponse() { IsSuccess = true, Data = "Add Successfully" };


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
            if (category == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Category" };
            }

            bool CategoryFound = seriesCategoryRepository.IsCategoryFound(CategoryId);

            if (CategoryFound)
            {
                List<Series> series = seriesCategoryRepository.GetAllSeriesInCategory(CategoryId);
                List<SeriesToGetDTO> seriesDTO = new List<SeriesToGetDTO>();
                foreach (var item in series)
                {
                    SeriesToGetDTO seriesToGetDTO = new SeriesToGetDTO()
                    {

                        SeriesId = item.Id,
                        CreatedYear = item.CreatedYear,
                        Description = item.Description,
                        DirectorID = item.DirectorID,
                        FilmSection = item.FilmSection,
                        LengthMinutes = item.LengthMinutes,
                        PosterImage = item.PosterImage,
                        Quality = item.Quality,
                        Revenue = item.Revenue,
                        Title = item.Title,
                        DirectorName = item.Director.Name,
                        Seasons = item.Seasons.Select(season => new SeasonsDTO
                        {
                            NumOfEpisodes = season.NumOfEpisodes,
                            Name = season.Name,
                            SeriesID = season.SeriesID // Assuming you want to include the SeriesID in each SeasonDTO
                        }).ToList(),

                    };

                    seriesDTO.Add(seriesToGetDTO);
                }
                return new GeneralResponse() { IsSuccess = true, Data = seriesDTO };
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Category is not Exist" };
            }

        }

        [HttpDelete]
        [Route("{CategoryId:int}/{SeriesId:int}")]
        public ActionResult<dynamic> DeleteSeriesFromCategory(int CategoryId, int SeriesId)
        {
            Category category = categoryRepository.GetCategoryById(CategoryId);
            Series series = seriesRepository.GetById(SeriesId);

            if (category != null && series != null)
            {
                bool IsSeriesFound = seriesCategoryRepository.IsSeriesFoundInCategory(SeriesId, CategoryId);
                if (IsSeriesFound)
                {
                    seriesCategoryRepository.DeleteSeriesFromCategory(CategoryId, SeriesId);
                    return new GeneralResponse() { IsSuccess = true, Data = "Series Deleted Successfully ): " };

                }
                else
                {
                    return new GeneralResponse() { IsSuccess = false, Data = "Series Not In this Category ): " };

                }
            }
            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = "Invalid Data ): " };
            }
        }

        [HttpPut("{CategoryId:int}/{SeriesId:int}")]

        public ActionResult<dynamic> EditCategorySeries(int CategoryId, int SeriesId, CategorySeriesDTO categorySeriesDTO)
        {
            if (ModelState.IsValid)
            {
                bool IsFoundInDB = seriesCategoryRepository.GetCategoriesAndSeries(CategoryId, SeriesId);
                if (IsFoundInDB)
                {
                    bool IsSeriesInCategory = seriesCategoryRepository.IsSeriesFoundInCategory(SeriesId, CategoryId);

                    if (IsSeriesInCategory)
                    {
                        bool IsFoundInDTO = seriesCategoryRepository.IsSeriesFoundInCategory( categorySeriesDTO.SeriesId, categorySeriesDTO.CategoryId);
                        if (!IsFoundInDTO)
                        {
                            CategorySeries categorySeries = seriesCategoryRepository.GetCategorySeries(CategoryId, SeriesId);

                            categorySeries.SeriesID = categorySeriesDTO.SeriesId;
                            categorySeries.CategoryID = categorySeriesDTO.CategoryId;
                            seriesCategoryRepository.Update(categorySeries);
                            seriesCategoryRepository.Save();
                            return new GeneralResponse()
                            {
                                IsSuccess = true,
                                Data = categorySeries
                            };
                        }
                        else
                        {
                            return new GeneralResponse()
                            {
                                IsSuccess = false,
                                Data = "series Already Exist In This Category"
                            };
                        }

                    }
                    else
                    {
                        return new GeneralResponse()
                        {
                            IsSuccess = false,
                            Data = "Series Already Exist"
                        };
                    }
                }
                else
                {
                    return new GeneralResponse()
                    {
                        IsSuccess = false,
                        Data = "Updated Failed"
                    };
                }

            }

            else
            {
                return new GeneralResponse() { IsSuccess = false, Data = " Invalid Data " };
            }


        }
    }
}
