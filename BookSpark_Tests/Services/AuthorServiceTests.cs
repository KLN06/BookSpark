using BookSpark.Data.Entities;
using BookSpark.Models.GenreViewModels;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;
using BookSpark.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSpark.Repositories;
using BookSpark.Models.AuthorViewModels;
using System.Drawing;
using BookSpark_Tests.Repositories;

namespace BookSpark_Tests.Services
{
    public class AuthorServiceTests
    {
        private readonly AuthorService authorService;

        private readonly Mock<IAuthorRepository> authorRepositoryMock;

        private readonly IEnumerable<Author> authorsInDatabase;

        public AuthorServiceTests()
        {
            authorsInDatabase = new[]
            {
                new Author(1, "Arthur C. Clarke", new DateTime(), "He was a great sci-fi author!", new List<Book>()),
                new Author(2, "Roald Dahl", new DateTime(), "He was a great children literature author!", new List<Book>()),
                new Author(3, "Stephen King", new DateTime(), "He was a great thriller author!", new List<Book>())
            };
            authorRepositoryMock = SetUpAuthorRepositoryMock();
            authorService = new AuthorService(authorRepositoryMock.Object);
        }

        #region Add

        [Test]
        public void GivenValidAuthor_WhenAddingAnAuthor_TheAuthorIsAdded()
        {
            var author = new AddAuthorViewModel()
            {
                Name = "Test Name",
                Birthdate = new DateTime(),
                Biography = "Test Bio"
            };

            authorService.Add(author);

            authorRepositoryMock.Verify(
                mock => mock.Add(It.Is<Author>(authorEntity =>
                authorEntity.Name == author.Name &&
                authorEntity.Birthdate == author.Birthdate &&
                authorEntity.Biography == author.Biography)),
                Times.Once);
        }
        #endregion

        #region GetAll
        [Test]
        
        public void GivenAuthorsExist_WhenGettingAllAuthors_AllAuthorsAreReturned()
        {
            var authors = authorService.GetAll();

            Assert.AreEqual(authorsInDatabase.Count(), authors.Count(), "Authors count is different than expected");
            foreach (var authorInDatabase in authorsInDatabase)
            {
                var authorExists = authors.Any(author =>
                    author.Id == authorInDatabase.Id &&
                    author.Name == authorInDatabase.Name &&
                    author.Birthdate == authorInDatabase.Birthdate &&
                    author.Biography == authorInDatabase.Biography);

                Assert.True(
                    authorExists,
                    $"Author with Id {authorInDatabase.Id} doesn't exist.");
            }
        }

        [Test]
        public void GivenNoAuthorsExist_WhenGettingAllAuthors_ReturnsEmptyCollection()
        {
            authorRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(new List<Author>());

            var authors = authorService.GetAll();

            Assert.AreEqual(0, authors.Count(), "List should be empty and it is not.");
        }

        #endregion

        #region Get

        [Test]
        public void GivenAnExistingId_WhenGettingAnAuthor_ReturnsTheAuthor()
        {
            var expectedAuthor = authorsInDatabase.First();

            var author = authorService.Get(expectedAuthor.Id);

            Assert.AreEqual(expectedAuthor.Id, author.Id, "Id not as expected");
            Assert.AreEqual(expectedAuthor.Name, author.Name, "Name not as expected");
            Assert.AreEqual(expectedAuthor.Birthdate, author.Birthdate, "Birthdate not as expected");
            Assert.AreEqual(expectedAuthor.Biography, author.Biography, "Biography not as expected");
        }

        #endregion

        #region GetEditable
        [Test]
        public void GivenAnExistingId_WhenGettingAnEditableAuthor_ReturnsTheEditableAuthor()
        {
            var expectedAuthor = authorsInDatabase.First();

            var editableAuthor = authorService.GetEditable(expectedAuthor.Id);

            Assert.AreEqual(expectedAuthor.Id, editableAuthor.Id, "Id not as expected");
            Assert.AreEqual(expectedAuthor.Name, editableAuthor.Name, "Name not as expected");
            Assert.AreEqual(expectedAuthor.Birthdate, editableAuthor.Birthdate, "Birthdate not as expected");
            Assert.AreEqual(expectedAuthor.Biography, editableAuthor.Biography, "Biography not as expected");
        }

        #endregion

        #region Edit
        [Test]
        public void GivenExistingAuthor_WhenEditingAuthor_UpdatesAuthor()
        {
            
            var editedAuthorViewModel = new EditAuthorViewModel 
            { 
                Id = 1, 
                Name = "Updated Author Name",
                Birthdate = new DateTime(),
                Biography = "Updated Author Biography",
                Books = new List<Book>()
            };
            var existingAuthor = authorsInDatabase.First(author => author.Id == editedAuthorViewModel.Id);

            authorService.Edit(editedAuthorViewModel);

            authorRepositoryMock.Verify(
                mock => mock.Edit(It.Is<Author>(author =>
                    author.Id == existingAuthor.Id &&
                    author.Name == editedAuthorViewModel.Name &&
                    author.Birthdate == editedAuthorViewModel.Birthdate &&
                    author.Biography == editedAuthorViewModel.Biography)),
                Times.Once);
        }
        #endregion

        #region Delete

        [Test]
        public void GivenExistingAuthor_WhenDeletingAuthor_DeletesAuthor()
        {
            var authorId = 1;

            authorService.Delete(authorId);

            authorRepositoryMock.Verify(repo => repo.Delete(authorId), Times.Once);
        }

        #endregion
        private Mock<IAuthorRepository> SetUpAuthorRepositoryMock()
        {
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            authorRepositoryMock.Setup(author => author.Add(It.IsAny<Author>()));

            authorRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(authorsInDatabase);

            authorRepositoryMock
                .Setup(mock => mock.Get(authorsInDatabase.First().Id))
                .Returns(authorsInDatabase.First());

            return authorRepositoryMock;
        }
    }
}
