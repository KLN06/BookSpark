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
    }
}
