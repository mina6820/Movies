using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.Repositories.CategoryRepo;
using Movies.Repositories.SeriesRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySeriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ISeriesRepository seriesRepository;

        public CategorySeriesController(ICategoryRepository categoryRepository, ISeriesRepository seriesRepository) 
        {
            this.categoryRepository = categoryRepository;
            this.seriesRepository = seriesRepository;
        }

        //public ActionResult<dynamic> AddSeriesToCategory()
        //{

        //}
    }
}
