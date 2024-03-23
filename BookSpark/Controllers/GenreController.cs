using BookSpark.Data.Enums;
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
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            var genres = genreService.GetAll();
           
            return View(genres);
        }

        public IActionResult Add()
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddGenreViewModel genre)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            genreService.Add(genre);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            genreService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            var genre = genreService.GetEditable(id);
            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(EditGenreViewModel genre)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(GenreAdminError));
            }
            genreService.Edit(genre);
            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult GenreAdminError()
        {
            return View();
        }
    }
}
