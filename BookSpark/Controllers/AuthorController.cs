using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.GenreViewModels;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookSpark.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IActionResult Index()
        {
            var authors = authorService.GetAll().ToList();
            return View(authors);

        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddAuthorViewModel author)
        {
            authorService.Add(author);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            authorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var author = authorService.GetEditable(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(EditAuthorViewModel author)
        {
            authorService.Edit(author);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            var author = authorService.Get(id);
            return View(author);
        }
    }
}
