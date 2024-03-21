using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSpark.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly IBookService bookService;
        private readonly UserManager<AppUser> userManager;

        public WishlistController(IWishlistService wishlistService, UserManager<AppUser> userManager)
        {
            this.wishlistService = wishlistService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var wishlistId = user.WishlistId;
            var books = wishlistService.GetAll(wishlistId).ToList();
            return View(books);
        }

        /*public IActionResult Add()
        {
            return RedirectToAction(nameof(Index));
        }*/
        
        public async Task<IActionResult> Add(int bookId)
        {
            var user = await userManager.GetUserAsync(User);
            var wishlistId = user.WishlistId;

            wishlistService.Add(bookId, wishlistId);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Remove(int bookId)
        {
            var user = await userManager.GetUserAsync(User);
            var wishlistId = user.WishlistId;

            wishlistService.Remove(bookId, wishlistId);
            return RedirectToAction(nameof(Index));
        }

    }
}
