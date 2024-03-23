using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            try
            {
                if (book is null)
                {
                    throw new ArgumentException("Book can't be null!");
                }

                bool doesAuthorExist = false;
                foreach (var author in context.Authors)
                {
                    if (book.AuthorId == author.Id)
                    {
                        doesAuthorExist = true;
                        break;
                    }
                }

                bool doesGenreExist = false;
                foreach (var genre in context.Genres)
                {
                    if (book.GenreId == genre.Id)
                    {
                        doesGenreExist = true;
                        break;
                    }
                }

                if (doesAuthorExist && doesGenreExist)
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot add a book with non-existing parameters");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
            if (book is not null)
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
