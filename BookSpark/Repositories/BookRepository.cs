using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookSpark.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;

        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Get(id);
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public void Edit(BookViewModel book)
        {
            var entity = Get(book.Id);

            entity.Title = book.Title;
            entity.Description = book.Description;
            entity.PublishedYear = book.PublishedYear;
            entity.Genre.Name = book.GenreName;
            entity.Author.Name = book.AuthorName;

            context.SaveChanges();
        }

        public Book Get(int id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == id);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books.Include("Author").Include("Genre").ToList();
        }
    }
}
