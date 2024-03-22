using BookSpark.Data.Enums;
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(AuthorsError));
            }
            var authors = authorService.GetAll().ToList();
            return View(authors);

        }

        public IActionResult Add()
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddAuthorViewModel author)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            authorService.Add(author);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            authorService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            var author = authorService.GetEditable(id);
            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(EditAuthorViewModel author)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            authorService.Edit(author);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(AuthorsAdminError));
            }
            var author = authorService.Get(id);
            return View(author);
        }

        public IActionResult AuthorsError()
        {
            return View();
        }

        public IActionResult AuthorsAdminError()
        {
            return View();
        }
    }
}
