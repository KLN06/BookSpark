using BookSpark.Models.BookViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IWishlistService
    {
        void Add(int bookId, string wishlistId);

        IEnumerable<BookViewModel> GetAll(string wishlistId);

        void Remove(int bookId, string wishlistId);
    }
}
