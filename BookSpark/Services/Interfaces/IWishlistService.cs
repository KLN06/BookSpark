using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IWishlistService
    {
        void Add(int bookId);

        public Task<IEnumerable<Book>> GetAll(string userId);

        void Remove(int bookId);

        public string GetUserId();
    }
}
