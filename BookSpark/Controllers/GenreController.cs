using BookSpark.Models.GenreViewModels;
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
            var genres = genreService.GetAll();
           
            return View(genres);
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

        public IActionResult Delete(int id)
        {
            genreService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var genre = genreService.Get(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
