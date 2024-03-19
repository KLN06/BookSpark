using BookSpark.Data.Entities;

namespace BookSpark.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        void Add(Genre genre);

        IEnumerable<Genre> GetAll();

        void Delete(int id);

        void Edit(Genre genre);

        Genre Get(int id);
    }
}
