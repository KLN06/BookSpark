using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSpark_Tests.Repositories
{
    public class BookRepositoryTests
    {
        private BookRepository bookRepository;
        private ApplicationDbContext applicationDbContext;

        [SetUp]
        public void SetUp()
        {
            applicationDbContext = SetUpApplicationDbContext();

            bookRepository = new BookRepository(applicationDbContext);
        }

        [TearDown]
        public void TearDown()
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Dispose();
        }


        #region Add
        [Test]
        public void GivenABook_WhenAddingBook_AddsBook()
        {
            var book = SeedBooks().First();
            
            foreach(var entity in bookRepository.GetAll())
            {
                bookRepository.Delete(entity.Id);
            }

            bookRepository.Add(book);

            var createdBook = applicationDbContext.Books.FirstOrDefault();

            Assert.NotNull(createdBook, "Book is null");
            Assert.AreEqual(book.Title, createdBook.Title, "Book title is different than expected");
            Assert.AreEqual(book.Description, createdBook.Description, "Book description is different than expected");
            Assert.AreEqual(book.PublishedYear, createdBook.PublishedYear, "Book published year is different than expected");
            Assert.AreEqual(book.GenreId, createdBook.GenreId, "Book genre ID is different than expected");
            Assert.AreEqual(book.AuthorId, createdBook.AuthorId, "Book author ID is different than expected");
            Assert.AreEqual(book.ImageLink, createdBook.ImageLink, "Book image link is different than expected");
        }

        [Test]
        public void AddingABook_BookIsNull_ThrowsAnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => bookRepository.Add(null));
            Assert.AreEqual("Book can't be null!", exception.Message, "Exception is different than expected.");
        }

        [Test]
        public void AddingABook_GenreAndOrAuthorDoesntExist_ThrowsAnArgumentException()
        {
            var book = new Book()
            {
                Title = "Test title",
                Description = "Test description",
                PublishedYear = 1900,
                AuthorId = -1,
                GenreId = -1,
                ImageLink = "Test link"
            };

            var exception = Assert.Throws<ArgumentException>(() => bookRepository.Add(book));
            Assert.AreEqual("Cannot add a book with non-existing parameters", exception.Message, "Exception is different than expected.");
        }
        #endregion

        #region GetAll
        [Test]
        public void WhenGettingAll_ReturnsAllBooks()
        {
            var expectedBooks = SeedBooks();

            var books = bookRepository.GetAll();

            Assert.AreEqual(expectedBooks, books, "Books are different than expected.");
        }
        #endregion

        #region Get
        [Test]
        public void GivenAnExistingId_WhenGettingBook_ReturnsTheBook()
        {
            var expectedBooks = SeedBooks();

            var expectedBook = expectedBooks.First();
            var book = bookRepository.Get(expectedBook.Id);

            Assert.NotNull(book, "Book is null");
            Assert.AreEqual(expectedBook.Title, book.Title, "Book title is different than expected");
            Assert.AreEqual(expectedBook.Description, book.Description, "Book description is different than expected");
            Assert.AreEqual(expectedBook.PublishedYear, book.PublishedYear, "Book published year is different than expected");
            Assert.AreEqual(expectedBook.GenreId, book.GenreId, "Book genre ID is different than expected");
            Assert.AreEqual(expectedBook.AuthorId, book.AuthorId, "Book author ID is different than expected");
            Assert.AreEqual(expectedBook.ImageLink, book.ImageLink, "Book image link is different than expected");
        }

        [Test]
        public void GettingABook_BookIsNull_ThrowsAnArgumentException()
        {
            var expectedBooks = SeedBooks();
            var expectedBookId = expectedBooks.First().Id;
            var differentThanExpectedBookId = 5;
            var exception = Assert.Throws<ArgumentException>(() => bookRepository.Get(differentThanExpectedBookId));

            Assert.AreEqual("No books!", exception.Message, "Exception different than expected.");
        }
        #endregion

        #region Delete
        [Test]
        public void GivenAnId_DeletingABook_DeletesIt()
        {
            var expectedBooks = SeedBooks();
            var id = expectedBooks.First().Id;
            bookRepository.Delete(id);
            var books = bookRepository.GetAll();
            foreach (var book in books)
            {
                Assert.That(id, !Is.EqualTo(book.Id), "Book has not been removed");
            }

        }

        [Test]
        public void GivenANonExistentId_DeletingAnAuthor_ThrowsAnArgumentException()
        {
            var expectedBooks = SeedBooks();
            var nonExistingId = 5;
            Assert.Throws<ArgumentException>(() => bookRepository.Delete(nonExistingId));
        }
        #endregion


        #region Edit
        [Test]
        public void Edit_ExistingBook_UpdatesBook()
        {
           
            var book = SeedBooks().First();

            var newBookTitle = "Edited title";
            var newDescription = "Edited description";
            var newPublishedYear = 1500;
            var newGenreId = 2;
            var newAuthorId = 2;
            var newImageLink = "Edited image link";
            
            book.Title = newBookTitle;
            book.Description = newDescription;
            book.PublishedYear = newPublishedYear;
            book.GenreId = newGenreId;
            book.AuthorId = newAuthorId;
            book.ImageLink = newImageLink;

            var bookEditable = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PublishedYear = newPublishedYear,
                GenreId = newGenreId,
                AuthorId = newAuthorId,
                ImageLink = newImageLink
            };

            bookRepository.Edit(bookEditable);


            var editedBook = applicationDbContext.Books.Find(book.Id);
            Assert.That(editedBook, Is.EqualTo(book), "Book is not updated");
        }

        [Test]
        public void Edit_NonExistingGenre_ThrowsException()
        {
            SeedBooks();
            var nonExistingBook = new BookSpark.Models.BookViewModels.EditBookViewModel()
            {
                Id = -1,
                Title = "Non-existing title",
                Description = "Non-existing description",
                PublishedYear = 1900,
                GenreId = 1,
                AuthorId = 1,
                ImageLink = "Image Link"
            };

            Assert.Throws<ArgumentException>(() => bookRepository.Edit(nonExistingBook), "Exception not thrown for editing non-existing book");
        }

        #endregion

        private IEnumerable<Book> SeedBooks()
        {

            var genres = new[]
            {
                new Genre {Id = 1, Name = "Genre 1" },
                new Genre {Id = 2, Name = "Genre 2" },
                new Genre {Id = 3, Name = "Genre 3" }
            };

            var authors = new[]
            {
                new Author(1, "Arthur C. Clarke", new DateTime(), "He was a great sci-fi author!", new List<Book>()),
                new Author(2, "Roald Dahl", new DateTime(), "He was a great children literature author!", new List<Book>()),
                new Author(3, "Stephen King", new DateTime(), "He was a great thriller author!", new List<Book>())
            };

            var books = new[]
            {
                new Book("2001: A Space Odyssey", "A really good sci-fi book", 1968, 1, 1, "2001ASOLink"),
                new Book("Matilda", "A really good childrens book", 1988, 2, 2, "MatildaLink"),
                new Book("It", "A really good horror book", 1986, 3, 3, "ITLink")
            };

            applicationDbContext.Genres.AddRange(genres);
            applicationDbContext.Authors.AddRange(authors);
            applicationDbContext.Books.AddRange(books);
            applicationDbContext.SaveChanges();

            foreach (var book in books)
            {
                var genre = applicationDbContext.Genres.ToList().Find(genre => genre.Id == book.GenreId);
                book.Genre = genre;
                var author = applicationDbContext.Authors.ToList().Find(author => author.Id == book.AuthorId);
                book.Author = author;
            }

            return books;
        }
        private ApplicationDbContext SetUpApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase("UnitTestsDb");

            return new ApplicationDbContext(options.Options);
        }
    }
}
