using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;

namespace BookSpark.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }
        public void Add(int bookId, string wishlistId)
        {
            wishlistRepository.Add(bookId, wishlistId);
        }

        public void Remove(int bookId, string wishlistId)
        {
            wishlistRepository.Remove(bookId, wishlistId);
        }
        public IEnumerable<BookViewModel> GetAll(string wishlistId)
        {
            var bookEntities = wishlistRepository.GetAll(wishlistId);

            var books = bookEntities.Select(book => new BookViewModel(book)).ToList();

            return books;
        }
    }
}
