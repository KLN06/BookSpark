using BookSpark.Models.AuthorViewModels;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            var authors = authorService.GetAll();
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
    }
}
