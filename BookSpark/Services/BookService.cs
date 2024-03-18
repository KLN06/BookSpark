using BookSpark.Data.Entities;
using BookSpark.Models;
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
    }
}
