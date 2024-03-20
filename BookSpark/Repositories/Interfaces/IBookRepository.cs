using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;

namespace BookSpark.Repositories.Interfaces
{
    public interface IBookRepository
    {
        void Add(Book book);

        IEnumerable<Book> GetAll();

        void Delete(int id);

        void Edit(EditBookViewModel book);

        Book Get(int id);
    }
}
