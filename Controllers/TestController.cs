using Microsoft.AspNetCore.Mvc;

namespace Movies.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
