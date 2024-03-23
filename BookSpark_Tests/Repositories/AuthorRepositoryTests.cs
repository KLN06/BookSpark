using BookSpark.Data.Entities;
using BookSpark.Data;
using BookSpark.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSpark.Repositories.Interfaces;

namespace BookSpark_Tests.Repositories
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

        [Test]
        public void AddingAnAuthor_AuthorIsNull_ThrowsAnArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => authorRepository.Add(null));
            Assert.AreEqual("Author cannot be null", exception.Message, "Exception is different than expected.");
        }
        #endregion

        #region GetAll
        [Test]
        public void WhenGettingAll_ReturnsAllAuthors()
        {
            var expectedAuthors = SeedAuthors();

            var authors = authorRepository.GetAll();

            Assert.AreEqual(expectedAuthors, authors, "Authors are different than expected.");
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

        [Test]
        public void GettingAnAuthor_AuthorIsNull_ThrowsAnArgumentException()
        {
            var expectedAuthors = SeedAuthors();
            var expectedAuthorId = expectedAuthors.First().Id;
            var differentThanExpectedAuthorId = 5;
            var exception = Assert.Throws<ArgumentException>(() => authorRepository.Get(differentThanExpectedAuthorId));

            Assert.AreEqual("Author cannot be null", exception.Message, "Exception different than expected.");
        }
        #endregion

        #region Delete
        [Test]
        public void GivenAnId_DeletingAnAuthor_DeletesThem()
        {
            var expectedAuthors = SeedAuthors();
            var id = expectedAuthors.First().Id;
            authorRepository.Delete(id);
            var authors = authorRepository.GetAll();
            foreach(var author in authors)
            {
                Assert.That(id, !Is.EqualTo(author.Id), "Author has not been removed");
            }

        }

        [Test]
        public void GivenANonExistentId_DeletingAnAuthor_ThrowsAnArgumentException()
        {
            var expectedAuthors = SeedAuthors();
            var nonExistingId = 5;
            Assert.Throws<ArgumentException>(()=> authorRepository.Delete(nonExistingId));
        }
        #endregion


        #region Edit
        [Test]
        public void Edit_ExistingAuthor_UpdatesAuthor()
        {
            var author = SeedAuthors().First();
            var newAuthorName = "Edited Name";
            var newdatetime = DateTime.Now;
            var newBiography = "Ëdited biography;";
            author.Name = newAuthorName;
            author.Birthdate = newdatetime;
            author.Biography = newBiography;

            authorRepository.Edit(author);

            var editedAuthor = applicationDbContext.Authors.Find(author.Id);
            Assert.That(editedAuthor, Is.EqualTo(author), "Author is not updated");
        }

        [Test]
        public void Edit_NonExistingGenre_ThrowsException()
        {
            SeedAuthors();
            var nonExistingAuthor = new Author 
            { Id = 5,
            Name = "Non-existing name",
            Birthdate = DateTime.Now,
            Biography = "Non-existing biography"
            };

            Assert.Throws<ArgumentException>(() => authorRepository.Edit(nonExistingAuthor), "Exception not thrown for editing non-existing author");
        }
        #endregion

        private IEnumerable<Author> SeedAuthors()
        {
            var authors = new[]
            {
                new Author("Arthur C. Clarke", new DateTime(), "He was a great sci-fi author!"),
                new Author("Roald Dahl", new DateTime(), "He was a great children literature author!"),
                new Author("Stephen King", new DateTime(), "He was a great thriller author!")
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
