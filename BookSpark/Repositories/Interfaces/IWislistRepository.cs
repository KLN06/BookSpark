using BookSpark.Data.Entities;

namespace BookSpark.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Task Add(int bookId, string userId);
        Task Remove(int bookId, string userId);
        public Task<Wishlist> GetWishlist(string userId);
        public Task<IEnumerable<Book>> GetAll(string userId);
    }
}
