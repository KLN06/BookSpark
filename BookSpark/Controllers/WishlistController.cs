using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace BookSpark.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly UserManager<AppUser> userManager;

        public WishlistController(IWishlistService wishlistService, UserManager<AppUser> userManager)
        {
            this.wishlistService = wishlistService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = wishlistService.GetUserId();
            var books = await wishlistService.GetAll(userId);
            if (books == null)
            {
                // обработка на случая, когато няма книги
                return View(new List<Book>());
            }
            return View(books.ToList());
        }

        public IActionResult Add(int bookId)
        {
            wishlistService.Add(bookId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int bookId)
        {
            wishlistService.Remove(bookId);
            return RedirectToAction(nameof(Index));
        }

    }
}
