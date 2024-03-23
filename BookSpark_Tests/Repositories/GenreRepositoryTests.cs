using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSpark_Tests.Repositories
{
    public class GenreRepositoryTests
    {
        private GenreRepository genreRepository;
        private ApplicationDbContext applicationContext;

        [SetUp]
        public void SetUp()
        {
            applicationContext = SetUpApplicationContext();
            genreRepository = new GenreRepository(applicationContext);
        }

        [TearDown]
        public void TearDown()
        {
            applicationContext.Database.EnsureDeleted();
            applicationContext.Dispose();
        }

        #region Add
        [Test]
        public void GivenGenre_AddNewGenre_AddsGenre()
        {
            var genre = new Genre { Name = "New Genre" };

            genreRepository.Add(genre);

            var createdGenre = applicationContext.Genres.LastOrDefault();
            Assert.That(createdGenre, Is.EqualTo(genre), "Genre is different than expected");
        }

        [Test]
        public void GivenNullGenre_NullGenre_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => genreRepository.Add(null), "Exception not thrown for non-existing genre ID");
            Assert.AreEqual("Genre cannot be null", exception.Message, "Exception message is different than expected");
        }
        #endregion


        #region GetAll
        [Test]
        public void GetAll_ReturnsAllGenres()
        {
            var expectedGenres = SeedGenres();

            var genres = genreRepository.GetAll();

            Assert.That(genres.Count(), Is.EqualTo(expectedGenres.Count()), "Count of genres is different than expected");
            CollectionAssert.AreEquivalent(expectedGenres, genres, "Genres are different than expected");
        }
        #endregion
        

        #region Get

        [Test]
        public void Get_ExistingGenreId_ReturnsGenre()
        {
            var expectedGenres = SeedGenres();
            var expectedGenre = expectedGenres.First();

            var genre = genreRepository.Get(expectedGenre.Id);

            Assert.That(genre, Is.EqualTo(expectedGenre), "Genre is different than expected");
        }

        [Test]
        public void Get_NonExistingGenreId_ThrowsException()
        {
            var genres = SeedGenres();
            var nonExistingId = -1;
            genres.FirstOrDefault(genres => genres.Id == nonExistingId);
            var exception = Assert.Throws<ArgumentException>(() => genreRepository.Get(nonExistingId), "Exception not thrown for non-existing genre ID");
            Assert.AreEqual("Genre cannot be null", exception.Message, "Exception message is different than expected");
        }
        #endregion

        #region Edit
        [Test]
        public void Edit_ExistingGenre_UpdatesGenre()
        {
            var expectedGenres = SeedGenres();
            var genreToEdit = expectedGenres.First();
            var newGenreName = "Edited Genre";
            genreToEdit.Name = newGenreName;

            genreRepository.Edit(genreToEdit);

            var editedGenre = applicationContext.Genres.Find(genreToEdit.Id);
            Assert.That(editedGenre.Name, Is.EqualTo(newGenreName), "Genre name is not updated");
        }

        [Test]
        public void Edit_NonExistingGenre_ThrowsException()
        {
            SeedGenres();
            var nonExistingGenre = new Genre { Id = -1, Name = "Non Existing Genre" };

            Assert.Throws<ArgumentException>(() => genreRepository.Edit(nonExistingGenre), "Exception not thrown for editing non-existing genre");
        }
        #endregion

        #region Delete

        [Test]
        public void Delete_ExistingGenreId_RemovesGenre()
        {
            var expectedGenres = SeedGenres();
            var genreToRemove = expectedGenres.First();

            genreRepository.Delete(genreToRemove.Id);

            var remainingGenres = applicationContext.Genres.ToList();
            Assert.That(remainingGenres.Contains(genreToRemove), Is.False, "Genre is not removed");
        }

        [Test]
        public void Delete_NonExistingGenreId_ThrowsException()
        {
            SeedGenres();
            var nonExistingId = -1;

            Assert.Throws<ArgumentException>(() => genreRepository.Delete(nonExistingId), "Exception not thrown for non-existing genre ID");
        }
        #endregion

        private IEnumerable<Genre> SeedGenres()
        {
            var genres = new[]
            {
                new Genre { Name = "Genre 1" },
                new Genre { Name = "Genre 2" },
                new Genre { Name = "Genre 3" }
            };

            applicationContext.Genres.AddRange(genres);
            applicationContext.SaveChanges();

            return genres;
        }

        private ApplicationDbContext SetUpApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("UnitTestsDb")
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
