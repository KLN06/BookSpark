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

        public WishlistRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task Add(int bookId, string userId)
        {
            var wishlist = await GetWishlist(userId);
            if (wishlist is null)
            {
                var user = context.Users.FirstOrDefault(u => u.Id == userId);
                wishlist = new Wishlist(userId, user, Guid.NewGuid().ToString());
                context.Wishlist.Add(wishlist);
                context.SaveChanges();
            }

            var wishlistItemExists = context.Wishlist
                .FirstOrDefault(wishlistEntity => wishlistEntity.Id == wishlist.Id &&
                wishlistEntity.Books.Any(book => book.Id == bookId));

            if (wishlistItemExists is null)
            {
                var book = context.Books
                    .Include("Author")
                    .Include("Genre")
                    .FirstOrDefault(b => b.Id == bookId);
                wishlist.Books.Add(book);
            }
            context.SaveChanges();

        }
        public async Task Remove(int bookId, string userId)
        {
            var wishlist = await GetWishlist(userId);

            if (wishlist is null)
            {
                throw new ArgumentException("Wishlist does not exist");
            }
            var wishlistItemExists = context.Wishlist.FirstOrDefault(a => a.Id == wishlist.Id && a.Books.Any(b => b.Id == bookId));
            if (wishlistItemExists is not null)
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

        public async Task<IEnumerable<Book>> GetAll(string userId)
        {
            var wishlist = await GetWishlist(userId);
            return wishlist?.Books ?? Enumerable.Empty<Book>();
        }
    }
}
