using BookSpark.Data.Entities;
using BookSpark.Models;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace BookSpark.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WishlistController(IWishlistService wishlistService, UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            this.wishlistService = wishlistService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if(userId == null) //if user does not exist return error view
            {
                return RedirectToAction(nameof(WishlistError));
            }
            var books = await wishlistService.GetAll(userId);
            if (books == null) // if there are no books return a view of a new list of books
            {
                return View(new List<Book>());
            }
            return View(books.ToList());
        }

        public IActionResult Add(int bookId)
        {
            if (!User.Identity.IsAuthenticated) // if user is not logged in/ registered return error view
            {
                return RedirectToAction(nameof(WishlistError));
            }
            var userId = GetUserId();
            wishlistService.Add(bookId, userId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int bookId)
        {
            if (!User.Identity.IsAuthenticated) // if user is not logged in/ registered return error view
            {
                return RedirectToAction(nameof(WishlistError));
            }
            var userId = GetUserId();
            wishlistService.Remove(bookId, userId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult WishlistError()
        {
            return View();
        }
    }
}
