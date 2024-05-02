using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Repositories.CategoryRepo;
using TestingMVC.Repo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                categoryRepository.Insert(category);
                categoryRepository.Save();
                return Ok(category);
            }
            return BadRequest("invalid category");
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(categoryRepository.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            Category category = categoryRepository.GetCategoryById(id);
            if(category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetCategoryByName(string name)
        {
            List<Category>? categories = categoryRepository.GetCategoryByName(name);
            if(categories == null)
                return NotFound();
            return Ok(categories);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditCategory(int id ,  Category category)
        {
            Category categoryfromDb = categoryRepository.GetCategoryById(id);
            if (categoryfromDb == null || categoryfromDb.Id != category.Id)
                return BadRequest("not found or the id in the category object is not equal the id from the object database");

            categoryfromDb.Name = category.Name;
            categoryRepository.Save();
            return Ok(category);
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            Category? category = categoryRepository.GetCategoryById(id);
            if(category == null)
                return NotFound();
            
            category.IsDeleted = true;
            categoryRepository.Save();
            return NoContent();
        }

    }

}
