using BookSpark.Data.Entities;
using BookSpark.Models;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories;
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
            if(userId == null)
            {
                return RedirectToAction(nameof(WishlistError));
            }
            var books = await wishlistService.GetAll(userId);
            if (books == null)
            {
                // if there are no books return a view of a new list of books
                return View(new List<Book>());
            }
            return View(books.ToList());
        }

        public IActionResult Add(int bookId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(WishlistError));
            }
            wishlistService.Add(bookId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int bookId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(WishlistError));
            }
            wishlistService.Remove(bookId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult WishlistError()
        {
            return View();
        }
    }
}
