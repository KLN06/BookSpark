using BookSpark.Data.Entities;
using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;

namespace BookSpark.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public void Add(AddBookViewModel book)
        {
            var bookEntity = new Book(book.Title, book.Description, book.PublishedYear, book.GenreId, book.AuthorId, book.ImageLink);

            bookRepository.Add(bookEntity);
        }

        public void Delete(int id)
        {
            bookRepository.Delete(id);
        }

        public void Edit(EditBookViewModel book)
        {
            bookRepository.Edit(book);
        }

        public BookViewModel Get(int id)
        {
            var book = bookRepository.Get(id);

            return new BookViewModel(book);
        }

        public IEnumerable<BookViewModel> GetAll()
        {
            var bookEntities = bookRepository.GetAll();

            var books = bookEntities.Select(book => new BookViewModel(book)).ToList();

            return books;
        }
    }
}
