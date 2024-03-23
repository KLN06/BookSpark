using BookSpark.Data.Entities;
using BookSpark.Models.GenreViewModels;
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

        public IEnumerable<GenreViewModel> GetAll()
        {
            var genreEntities = genreRepository.GetAll();

            var genres = genreEntities
               .Select(genre => new GenreViewModel(genre.Id, genre.Name));
            return genres;
        }
        
        public GenreViewModel Get(int id)
        {
            var genre = genreRepository.Get(id);
            return new GenreViewModel(genre.Id, genre.Name);

        }
        public EditGenreViewModel GetEditable(int id)
        {
            var genre = genreRepository.Get(id);
            return new EditGenreViewModel(genre.Id, genre.Name);

        }

        public void Edit(EditGenreViewModel genre)
        {
            var genreEntity = new Genre(genre.Id, genre.Name);

            genreRepository.Edit(genreEntity);
        }

        public void Delete(int id)
        {
            genreRepository.Delete(id);
        }
    }
}
