using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs;
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
        public ActionResult<GeneralResponse> AddCategory(CategoryDTO categoryDto)
        {
            
            if(ModelState.IsValid)
            {
                Category category = new Category() { Name = categoryDto.Name } ;
                categoryRepository.Insert(category);
                categoryRepository.Save();
               
               return new GeneralResponse() { IsSuccess = true , Data = category};
            }
            return new GeneralResponse() { IsSuccess= false , Data = "Invalid Name"};
        }

        [HttpGet]
        public ActionResult<GeneralResponse> GetAllCategories()
        {
            List<Category>? categories = categoryRepository.GetAll();
            if (categories == null)
                return new GeneralResponse() { IsSuccess = false, Data = "No Categories yet" };
            return new GeneralResponse() { IsSuccess = true ,Data = categories};
        }

        [HttpGet("{id:int}")]
        public ActionResult<GeneralResponse> GetCategoryById(int id)
        {
            Category category = categoryRepository.GetCategoryById(id);
            if(category == null)
                return new GeneralResponse() { IsSuccess = false , Data = $"No Category With ID : {id}"};

            return new GeneralResponse() { IsSuccess = true ,Data = category};
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<GeneralResponse> GetCategoryByName(string name)
        {
            List<Category>? categories = categoryRepository.GetCategoryByName(name);
            if (categories == null)
                return new GeneralResponse() { IsSuccess = false, Data = "No Category Found" };

            return new GeneralResponse() { IsSuccess = true, Data = categories};
        }

        [HttpPut("{id:int}")]
        public ActionResult<GeneralResponse> EditCategory(int id ,  CategoryDTO category)
        {
            Category categoryfromDb = categoryRepository.GetCategoryById(id);
            if (categoryfromDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Category With ID : {id}" };

            categoryfromDb.Name = category.Name;
            categoryRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = categoryfromDb};
        }

        [HttpDelete]
        public ActionResult<GeneralResponse> DeleteCategory(int id)
        {
            Category? category = categoryRepository.GetCategoryById(id);
            if(category == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Category With ID : {id}" };

            category.IsDeleted = true;
            categoryRepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = "Deleted Successfully" };
        }

    }

}
