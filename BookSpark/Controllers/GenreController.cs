using BookSpark.Models;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookSpark.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddGenreViewModel genre)
        {
            genreService.Add(genre);
            return RedirectToAction(nameof(Index));
        }
    }
}
