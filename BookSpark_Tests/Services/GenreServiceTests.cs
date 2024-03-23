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

namespace BookSpark_Tests.Services
{
    public class GenreServiceTests
    {
        private IGenreService genreService;
        private Mock<IGenreRepository> genreRepositoryMock;
        private readonly IEnumerable<Genre> genresInDatabase;

        public GenreServiceTests()
        {
            genresInDatabase = new List<Genre>
            {
                new Genre { Id = 1, Name = "Science Fiction" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Mystery" }
            };

        }

        [SetUp]
        public void SetUp()
        {
            genreRepositoryMock = SetUpGenreRepositoryMock();
            genreService = new GenreService(genreRepositoryMock.Object);
        }
        private Mock<IGenreRepository> SetUpGenreRepositoryMock()
        {
            var genreRepositoryMock = new Mock<IGenreRepository>();

            genreRepositoryMock.Setup(mock => mock.Add(It.IsAny<Genre>()));

            genreRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(genresInDatabase);

            genreRepositoryMock
                .Setup(mock => mock.Get(genresInDatabase.First().Id))
                .Returns(genresInDatabase.First());

            genreRepositoryMock
                .Setup(mock => mock.Edit(It.IsAny<Genre>()));

            return genreRepositoryMock;
        }

        #region Add

        [Test]
        public void GivenGenreExist_WhenAddingAGenre_AddsGenre()
        {
            var genre = new AddGenreViewModel { Name = "Science Fiction" };

            genreService.Add(genre);

            genreRepositoryMock.Verify(
               mock => mock.Add(It.Is<Genre>(genreEntity =>
                   genreEntity.Name == genre.Name)),
               Times.Once);
        }

        #endregion

        #region GetAll
        [Test]
        public void GivenGenresExist_WhenGettingAllGenres_AllGenresAreReturned()
        {
            var genres = genreService.GetAll();

            Assert.That(
                genres.Count(), Is.EqualTo(genresInDatabase.Count()),
                "Genres count different than expected");

            foreach (var genreInDatabase in genresInDatabase)
            {
                var genreExists = genres.Any(genre =>
                        genre.Id == genreInDatabase.Id &&
                        genre.Name == genreInDatabase.Name);

                Assert.True(
                    genreExists,
                    $"Genre with Id {genreInDatabase.Id} doesn't exist");
            }
        }

        [Test]
        public void GivenNoGenresExist_WhenGettingAllGenres_ReturnsEmptyCollection()
        {
            genreRepositoryMock
                .Setup(mock => mock.GetAll())
                .Returns(new List<Genre>());

            var products = genreService.GetAll();

            Assert.AreEqual(0, products.Count());
        }

        #endregion

        [Test]
        public void GivenAnExistingId_WhenGettingAGenre_ReturnsTheGenre()
        {
            var expectedGenreEntity = genresInDatabase.First();

            var genre = genreService.Get(expectedGenreEntity.Id);

            Assert.AreEqual(expectedGenreEntity.Id, genre.Id);
            Assert.AreEqual(expectedGenreEntity.Name, genre.Name);
        }

        [Test]
        public void GivenAnExistingId_WhenGettingAGenreEditable_ReturnsCorrectEditableGenre()
        {
            var expectedGenreEntity = genresInDatabase.First();

            var editableGenre = genreService.GetEditable(expectedGenreEntity.Id);

            Assert.AreEqual(expectedGenreEntity.Id, editableGenre.Id);
            Assert.AreEqual(expectedGenreEntity.Name, editableGenre.Name);
        }

        [Test]
        public void GivenExistingGenre_WhenEditingGenre_UpdatesGenre()
        {
            var editedGenreViewModel = new EditGenreViewModel { Id = 1, Name = "Updated Genre Name" };
            var existingGenre = genresInDatabase.First(genre => genre.Id == editedGenreViewModel.Id);

            genreService.Edit(editedGenreViewModel);

            genreRepositoryMock.Verify(
                mock => mock.Edit(It.Is<Genre>(genre =>
                    genre.Id == existingGenre.Id &&
                    genre.Name == editedGenreViewModel.Name)),
                Times.Once);
        }

        [Test]
        public void GivenExistingGenre_WhenDeletingGenre_DeletesGenre()
        {
            var genreId = 1;

            genreService.Delete(genreId);

            genreRepositoryMock.Verify(repo => repo.Delete(genreId), Times.Once);
        }
    }
}
