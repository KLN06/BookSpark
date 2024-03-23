using BookSpark.Data.Entities;

namespace BookSpark.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Task Add(int bookId);
        Task Remove(int bookId);
        public string GetUserId();
        public Task<Wishlist> GetWishlist(string userId);
        public Task<IEnumerable<Book>> GetAll(string userId);
    }
}
