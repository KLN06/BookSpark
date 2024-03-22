using BookSpark.Data.Entities;
using BookSpark.Data;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using BookSpark.Models.BookViewModels;
using System.Net;

namespace BookSpark.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WishlistRepository(ApplicationDbContext context,UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task Add(int bookId)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user is not logged-in");
            }
            Wishlist? wishlist = await GetWishlist(userId);
            if (wishlist is null)
            {
                var user = context.Users.Find(userId);
                wishlist = new Wishlist(userId, user, Guid.NewGuid().ToString());
                context.Wishlist.Add(wishlist);
            }
            context.SaveChanges();

            var wishlistItem = context.Wishlist.FirstOrDefault(a => a.Id == wishlist.Id && a.Books.Any(b => b.Id == bookId));
            if (wishlistItem is not null)
            {
                //message   
            }
            else
            {
                var book = context.Books
                    .Include("Author")
                    .Include("Genre")
                    .FirstOrDefault(b => b.Id == bookId);
                wishlist.Books.Add(book);
            }
            context.SaveChanges();

        }
        public async Task Remove(int bookId)
        {
            string userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user is not logged-in");
            }
            var wishlist = await GetWishlist(userId);
            if (wishlist is null)
            {
                throw new ArgumentException("Wishlist is empty");
            }

            var wishlistItem = context.Wishlist.FirstOrDefault(a => a.Id == wishlist.Id && a.Books.Any(b => b.Id == bookId));
            if (wishlistItem is not null)
            {
                var book = context.Books.Find(bookId);
                wishlist.Books.Remove(book);
            }
            else
            {
                throw new ArgumentException("Book does not exist");
            }
            context.SaveChanges();
        }
        public async Task<Wishlist>? GetWishlist(string userId)
        {
            foreach(var wishlist in context.Wishlist.Include("AppUser").Include("Books"))
            {
                if(wishlist.AppUserId == userId)
                {
                    return wishlist;
                }
            }
            return null;
                      
        }

        public string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }

        public async Task<IEnumerable<Book>> GetAll(string userId)
        {
            var wishlist = await GetWishlist(userId);
            return wishlist?.Books ?? Enumerable.Empty<Book>();
        }
    }
}
