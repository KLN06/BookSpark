using BookSpark.Data.Entities;

namespace BookSpark.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        void Add(int bookId, string wishListId);
        void Remove(int bookId, string wishListId);
        public Wishlist Get(string id);
        public IEnumerable<Book> GetAll(string id);
    }
}
