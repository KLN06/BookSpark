using BookSpark.Data.Entities;
using BookSpark.Data;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookSpark.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IBookRepository bookRepository;

        public WishlistRepository(ApplicationDbContext context,IBookRepository bookRepository)
        {
            this.context = context;
            this.bookRepository = bookRepository;
        }
        public void Add(int bookId, string wishlistId)
        {
            var wishlist = Get(wishlistId);
            var book = bookRepository.Get(bookId);
            // променлива, която проверява дали книгата вече се съдържа в списъка ни с книги
            bool isInWishlist = false;
            /*foreach(var bookItem in wishlist.Books)
            {
                if (bookItem.Id == bookId)
                {
                    isInWishlist = true;
                }
            }*/
            if(!isInWishlist)
            {
                wishlist.Books.Add(book);
                context.SaveChanges();
            }
            
        }
        public void Remove(int bookId, string wishlistId)
        {
            var wishList = Get(wishlistId);
            var book = bookRepository.Get(bookId);
            wishList.Books.Remove(book);
            context.SaveChanges();
        }

        public Wishlist Get(string id)
        {
            var wishlist = context.Wishlist.FirstOrDefault(x => x.Id == id);
            if (wishlist is null)
            {
                throw new ArgumentException("No books in your wishlist");
            }
            return wishlist;
        }
        public IEnumerable<Book> GetAll(string id)
        {
            var wishlist = Get(id);
            return wishlist.Books.ToList();
        }
    }
}
