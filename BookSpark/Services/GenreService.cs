using BookSpark.Data.Entities;
using BookSpark.Models;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;

namespace BookSpark.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public void Add(AddGenreViewModel genre)
        {
            var genreEntity = new Genre(genre.Name);

            genreRepository.Add(genreEntity);
        }
    }
}
