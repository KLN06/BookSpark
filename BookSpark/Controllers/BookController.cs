using BookSpark.Data.Enums;
using BookSpark.Models.BookViewModels;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookSpark.Controllers
{
    public class BookController : Controller 
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        public IActionResult Index()
        {
            var books = bookService.GetAll().ToList();
            return View(books);
        }
        public IActionResult Add()
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(BookAdminError));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel book)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(BookAdminError));
            }
            bookService.Add(book);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(BookAdminError));
            }
            bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(BookAdminError));
            }
            var bookViewModel = bookService.Get(id);
            var editBookViewModel = new EditBookViewModel(
                bookViewModel.Title,
                bookViewModel.Description,
                bookViewModel.PublishedYear,
                bookViewModel.GenreId,
                bookViewModel.AuthorId,
                bookViewModel.ImageLink);

            return View(editBookViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel book)
        {
            if (!User.IsInRole(Roles.Admin.ToString()))
            {
                return RedirectToAction(nameof(BookAdminError));
            }
            bookService.Edit(book);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(BookError));
            }
            var book = bookService.Get(id);
            return View(book);
        }

        public IActionResult BookError()
        {
            return View();
        }
        public IActionResult BookAdminError()
        {
            return View();
        }
    }
}
