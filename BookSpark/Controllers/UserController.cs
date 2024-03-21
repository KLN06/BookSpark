using Microsoft.AspNetCore.Mvc;

namespace BookSpark.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
