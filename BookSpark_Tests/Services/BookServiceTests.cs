using BookSpark.Data.Entities;
using BookSpark.Models.AuthorViewModels;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services;
using BookSpark.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSpark_Tests.Services
{
    public class BookServiceTests
    {
        private readonly BookService bookService;

        private readonly Mock<IBookRepository> bookRepositoryMock;

        private readonly IEnumerable<Book> booksInDatabase;

        private readonly IEnumerable<Author> authorsInDatabase;

        private readonly IEnumerable<Genre> genresInDatabase;

        public BookServiceTests()
        {

            genresInDatabase = new List<Genre>
            {
                new Genre { Id = 1, Name = "Children literature" },
                new Genre { Id = 2, Name = "Horror" },
                new Genre { Id = 3, Name = "Science Fiction" }
            };

            authorsInDatabase = new[]
            {
                new Author(1, "Arthur C. Clarke", new DateTime(), "He was a great sci-fi author!", new List<Book>()),
                new Author(2, "Roald Dahl", new DateTime(), "He was a great children literature author!", new List<Book>()),
                new Author(3, "Stephen King", new DateTime(), "He was a great thriller author!", new List<Book>())
            };

            booksInDatabase = new[]
            {
                new Book()
                {
                    Id = 1,
                    Title = "Matilda", 
                    Description = "A great children's book",
                    PublishedYear = 1988,
                    GenreId = 1,
                    AuthorId = 2,
                    ImageLink = "Link"
                },
                new Book()
                {
                    Id = 2,
                    Title = "It",
                    Description = "A great horror book",
                    PublishedYear = 1986,
                    GenreId = 2,
                    AuthorId = 3,
                    ImageLink = "Link"
                },
                new Book()
                {
                    Id = 1,
                    Title = "2001: A Space Odyssey",
                    Description = "A great sci fi book",
                    PublishedYear = 1968,
                    GenreId = 3,
                    AuthorId = 1,
                    ImageLink = "Link"
                },
            };
            foreach(var book in booksInDatabase)
            {
                book.Genre = genresInDatabase.FirstOrDefault(g => g.Id == book.GenreId);
                book.Author = authorsInDatabase.FirstOrDefault(a => a.Id == book.AuthorId);
            }
            bookRepositoryMock = SetUpBookRepositoryMock();
            bookService = new BookService(bookRepositoryMock.Object);
        }

        #region Add

        [Test]
        public void GivenValidBook_WhenAddingABook_TheBookIsAdded()
        {
            var book = new AddBookViewModel()
            {
                Title = "Title",
                Description = "Description",
                PublishedYear = 2000,
                GenreId = 1,
                AuthorId = 1,
                ImageLink = "Image link"
            };

            bookService.Add(book);

            bookRepositoryMock.Verify(
                mock => mock.Add(It.Is<Book>(bookEntity =>
                bookEntity.Title == book.Title &&
                bookEntity.Description == book.Description &&
                bookEntity.PublishedYear == book.PublishedYear &&
                bookEntity.GenreId == book.GenreId &&
                bookEntity.AuthorId == book.AuthorId &&
                bookEntity.ImageLink == book.ImageLink)),
                Times.Once);
        }
        #endregion

        #region GetAll
        [Test]

        public void GivenBooksExist_WhenGettingAllBooks_AllBooksAreReturned()
        {
            var books = bookService.GetAll().ToList();

            Assert.AreEqual(booksInDatabase.Count(), books.Count(), "Books count is different than expected");
            foreach (var bookInDatabase in booksInDatabase)
            {
                var bookExists = books.Any(book =>
                    book.Id == bookInDatabase.Id &&
                    book.Title == bookInDatabase.Title &&
                    book.Description == bookInDatabase.Description &&
                    book.PublishedYear == bookInDatabase.PublishedYear &&
                    book.GenreId == bookInDatabase.GenreId &&
                    book.AuthorId == bookInDatabase.AuthorId &&
                    book.ImageLink == bookInDatabase.ImageLink
                    );

                Assert.True(
                    bookExists,
                    $"Book with Id {bookInDatabase.Id} doesn't exist.");
            }
        }

        [Test]
        public void GivenNoBooksExist_WhenGettingAllBooks_ReturnsEmptyCollection()
        {
            bookRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(new List<Book>());

            var books = bookService.GetAll();

            Assert.AreEqual(0, books.Count(), "List should be empty and it is not.");
        }

        #endregion


        #region Get

        [Test]
        public void GivenAnExistingId_WhenGettingABook_ReturnsTheBook()
        {
            var expectedBook = booksInDatabase.First();

            var book = bookService.Get(expectedBook.Id);

            Assert.AreEqual(expectedBook.Id, book.Id, "Id not as expected");
            Assert.AreEqual(expectedBook.Title, book.Title, "Name not as expected");
            Assert.AreEqual(expectedBook.Description, book.Description, "Description not as expected");
            Assert.AreEqual(expectedBook.PublishedYear, book.PublishedYear, "Published year not as expected");
            Assert.AreEqual(expectedBook.GenreId, book.GenreId, "Genre ID not as expected");
            Assert.AreEqual(expectedBook.AuthorId, book.AuthorId, "Author ID not as expected");
            Assert.AreEqual(expectedBook.ImageLink, book.ImageLink, "Image link not as expected");
        }

        #endregion


        #region Edit
        [Test]
        public void GivenExistingBook_WhenEditingBook_UpdatesBook()
        {
            // Arrange
            var editedBookViewModel = new EditBookViewModel
            {
                Id = 1,
                Title = "New Title",
                Description = "New Description",
                PublishedYear = 2000,
                GenreId = 2, // Assuming this is a different genre
                AuthorId = 2, // Assuming this is a different author
                ImageLink = "New Image Link"
            };

            // Get the book from the database that corresponds to the edited book
            var entity = booksInDatabase.First(book => book.Id == editedBookViewModel.Id);

            // Create a copy of the existing book
            var existingBook = new Book()
            {
                Title = entity.Title,
                Description = entity.Description,
                PublishedYear = entity.PublishedYear,
                GenreId = entity.GenreId,
                AuthorId = entity.AuthorId,
                ImageLink = entity.ImageLink,
            };

            // Act
            bookService.Edit(editedBookViewModel);

            // Assert

            // Verify that the book repository's Edit method was called with the correct parameters
            bookRepositoryMock.Verify(
                mock => mock.Edit(It.Is<EditBookViewModel>(bookEntity =>
                bookEntity.Id == editedBookViewModel.Id &&
                bookEntity.Title == editedBookViewModel.Title &&
                bookEntity.Description == editedBookViewModel.Description &&
                bookEntity.PublishedYear == editedBookViewModel.PublishedYear &&
                bookEntity.GenreId == editedBookViewModel.GenreId &&
                bookEntity.AuthorId == editedBookViewModel.AuthorId &&
                bookEntity.ImageLink == editedBookViewModel.ImageLink)),
                Times.Once);
        }

        #endregion

        #region Delete
        [Test]
        public void GivenExistingBook_WhenDeletingBook_DeletesBook()
        {
            var bookId = 1;

            bookService.Delete(bookId);

            bookRepositoryMock.Verify(repo => repo.Delete(bookId), Times.Once);
        }
        #endregion


        private Mock<IBookRepository> SetUpBookRepositoryMock()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();

            bookRepositoryMock.Setup(book => book.Add(It.IsAny<Book>()));

            bookRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(booksInDatabase);

            bookRepositoryMock
                .Setup(mock => mock.Get(booksInDatabase.First().Id))
                .Returns(booksInDatabase.First());

            bookRepositoryMock
                .Setup(mock => mock.Edit(It.IsAny<EditBookViewModel>()))
                .Verifiable();

            return bookRepositoryMock;
        }


    }
}
