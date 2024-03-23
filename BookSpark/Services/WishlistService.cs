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
        public void Add(int bookId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user is not logged-in");
            }
            wishlistRepository.Add(bookId, userId);
        }

        public void Remove(int bookId, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("user is not logged-in");
            }
            wishlistRepository.Remove(bookId, userId);
        }

        public async Task<IEnumerable<Book>> GetAll(string userId)
        {
            return await wishlistRepository.GetAll(userId);
        }
    }
}
