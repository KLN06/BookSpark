using Microsoft.AspNetCore.Mvc;

namespace BookSpark.Controllers
{
    public class BookConroller : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
