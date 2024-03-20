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
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddBookViewModel book)
        {
            bookService.Add(book);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
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
            bookService.Edit(book);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            var book = bookService.Get(id);
            return View(book);
        }
    }
}
