using BookSpark.Data.Entities;
using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.BookViewModels;

namespace BookSpark.Services.Interfaces
{
    public interface IBookService
    {
        void Add(AddBookViewModel book);

        IEnumerable<BookViewModel> GetAll();
    }
}
