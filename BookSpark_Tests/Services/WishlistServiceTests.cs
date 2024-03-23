using BookSpark.Data.Entities;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookSpark_Tests.Services
{
    public class WishlistServiceTests
    {
        private WishlistService wishlistService;
        private Mock<IWishlistRepository> wishlistRepositoryMock;
        private readonly IEnumerable<Book> booksInDatabase;

        public WishlistServiceTests()
        {
            booksInDatabase = new[]
            {
                new Book()
                {
                    Title = "Title1",
                    Description = "Description",
                    PublishedYear = 2000,
                    Genre = new Genre()
                    {
                        Name = "GenreName"
                    },
                    GenreId = 1,
                    Author = new Author()
                    {
                        Name = "AuthorName",
                        Birthdate = DateTime.Now,
                        Biography = "bio"
                    },
                    AuthorId = 1,
                    ImageLink = "image"
                },
                new Book()
                {
                    Title = "Title1",
                    Description = "Description",
                    PublishedYear = 2000,
                    Genre = new Genre()
                    {
                        Name = "GenreName"
                    },
                    GenreId = 1,
                    Author = new Author()
                    {
                        Name = "AuthorName",
                        Birthdate = DateTime.Now,
                        Biography = "bio"
                    },
                    AuthorId = 1,
                    ImageLink = "image"
                }
            };
        }

        [SetUp]
        public void SetUp()
        {
            wishlistRepositoryMock = SetUpWishlistRepositoryMock();
            wishlistService = new WishlistService(wishlistRepositoryMock.Object);
        }
        #region Add

        [Test]
        public void GivenValidBook_WhenAddingABook_TheBookIsAdded()
        {
            var userId = "123";
            var bookId = 1;

            wishlistService.Add(bookId, userId);

            wishlistRepositoryMock.Verify(
                mock => mock.Add(It.Is<int>(id => id == bookId), It.Is<string>(uid => uid == userId)),
                Times.Once);
        }

        [Test]
        public void GivenInvalidUserId_WhenAddingABook_ThrowsException()
        {
            var userId = string.Empty;
            var bookId = 1;

            Assert.Throws<Exception>(() => wishlistService.Add(bookId, userId));
        }

        #endregion

        #region Remove
        [Test]
        public void GivenValidBook_WhenRemovingABook_TheBookIsRemoved()
        {
            var userId = "123";
            var bookId = 1;

            wishlistService.Remove(bookId, userId);

            wishlistRepositoryMock.Verify(
                mock => mock.Remove(It.Is<int>(id => id == bookId), It.Is<string>(uid => uid == userId)),
                Times.Once);
        }

        [Test]
        public void GivenInvalidUserId_WhenRemovingABook_ThrowsException()
        {
            var userId = string.Empty;
            var bookId = 1;

            Assert.Throws<Exception>(() => wishlistService.Remove(bookId, userId));
        }
        #endregion

        #region GetAll
        [Test]
        public async Task GivenValidUserId_WhenGettingAllBooks_ReturnsAllBooksInWishlist()
        {
            var userId = "123";

            wishlistRepositoryMock
                .Setup(mock => mock.GetAll(It.Is<string>(uid => uid == userId)))
                .ReturnsAsync(booksInDatabase);

            var books = await wishlistService.GetAll(userId);

            Assert.AreEqual(booksInDatabase.Count(), books.Count());

            foreach (var bookInDatabase in booksInDatabase)
            {
                var bookExists = books.Any(book =>
                        book.Id == bookInDatabase.Id &&
                        book.Title == bookInDatabase.Title);

                Assert.True(
                    bookExists,
                    $"Book with Id {bookInDatabase.Id} doesn't exist");
            }
        }

        #endregion

        private Mock<IWishlistRepository> SetUpWishlistRepositoryMock()
        {
            var wishlistRepositoryMock = new Mock<IWishlistRepository>();

            wishlistRepositoryMock.Setup(mock => mock.Add(It.IsAny<int>(), It.IsAny<string>()));
            wishlistRepositoryMock.Setup(mock => mock.Remove(It.IsAny<int>(), It.IsAny<string>()));
            wishlistRepositoryMock.Setup(mock => mock.GetAll(It.IsAny<string>()))
                .ReturnsAsync(booksInDatabase);

            return wishlistRepositoryMock;
        }
    }
}
