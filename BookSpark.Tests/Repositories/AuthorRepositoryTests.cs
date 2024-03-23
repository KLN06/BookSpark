using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSpark.Tests.Repositories
{
    public class AuthorRepositoryTests
    {
        private AuthorRepository authorRepository;

        private ApplicationDbContext applicationDbContext;

        [SetUp]
        public void SetUp()
        {
            applicationDbContext = SetUpApplicationDbContext();

            authorRepository = new AuthorRepository(applicationDbContext);
        }

        [TearDown]
        public void TearDown()
        {
            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Dispose();
        }
        #region Add
        [Test]
        public void GivenAnAuthor_WhenAddingAuthor_AddsAuthor()
        {
            DateTime datetime = new DateTime();
            var author = new Author("Arthur C. Clarke", datetime, "He was a great author!");

            authorRepository.Add(author);

            var createdAuthor = applicationDbContext.Authors.LastOrDefault();

            Assert.NotNull(createdAuthor, "Author is null");
            Assert.AreEqual(author.Name, createdAuthor.Name, "Author name is different than expected");
            Assert.AreEqual(author.Birthdate, createdAuthor.Birthdate, "Author birthdate is different than expected");
            Assert.AreEqual(author.Biography, createdAuthor.Biography, "Author biography is different than expected");
        }
        #endregion

        #region GetAll
        [Test]
        public void WhenGettingAll_ReturnsAllAuthors()
        {
            var expectedAuthors = SeedAuthors();

            var authors = authorRepository.GetAll();

            Assert.AreEqual(expectedAuthors, authors);
        }

        #endregion

        #region Get
        [Test]
        public void GivenAnExistingId_WhenGettingAuthor_ReturnsTheAuthor()
        {
            var expectedAuthors = SeedAuthors();

            var expectedAuthor = expectedAuthors.First();
            var author = authorRepository.Get(expectedAuthor.Id);

            Assert.NotNull(author, "Author is null");
            Assert.AreEqual(expectedAuthor.Name, author.Name, "Author name is different than expected");
            Assert.AreEqual(expectedAuthor.Birthdate, author.Birthdate, "Author birthdate is different than expected");
            Assert.AreEqual(expectedAuthor.Biography, author.Biography, "Author biography is different than expected");
        }
        #endregion

        private IEnumerable<Author> SeedAuthors()
        {

            DateTime datetime1 = new DateTime();
            DateTime datetime2 = new DateTime();
            DateTime datetime3 = new DateTime();
            var authors = new[]
            {
                new Author("Arthur C. Clarke", datetime1, "He was a great sci-fi author!"),
                new Author("Roald Dahl", datetime2, "He was a great children literature author!"),
                new Author("Stephen King", datetime1, "He was a great thriller author!")
            };
            applicationDbContext.Authors.AddRange(authors);
            applicationDbContext.SaveChanges();

            return authors;
        }
        private ApplicationDbContext SetUpApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase("UnitTestsDb");

            return new ApplicationDbContext(options.Options);
        }
    }
}
