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
            if(book is null)
            {
                throw new ArgumentException("Book can't be null!");
            }
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Get(id);
            if(book != null)
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }

        public void Edit(EditBookViewModel book)
        {
            var entity = Get(book.Id);

            entity.Title = book.Title;
            entity.Description = book.Description;
            entity.PublishedYear = book.PublishedYear;
            entity.GenreId = book.GenreId;
            entity.AuthorId = book.AuthorId;
            entity.ImageLink = book.ImageLink;

            context.SaveChanges();
        }

        public Book Get(int id)
        {
            var book = context.Books.Include("Author").Include("Genre").FirstOrDefault(x => x.Id == id);
            if (book is null)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            throw new ArgumentException("No books!");
        }

        public IEnumerable<Book> GetAll()
        {
            return context.Books.Include("Author").Include("Genre").ToList();
        }
    }
}
