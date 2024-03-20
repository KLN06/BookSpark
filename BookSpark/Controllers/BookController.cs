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
            var book = bookService.Get(id);
            var book1 = new EditBookViewModel(book.Title, book.Description, book.PublishedYear, book.GenreId, book.AuthorId, book.ImageLink);

            return View(book1);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel book)
        {
            bookService.Edit(book);

            return RedirectToAction(nameof(Index));
        }
    }
}
