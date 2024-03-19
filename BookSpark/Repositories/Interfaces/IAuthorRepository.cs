using BookSpark.Data.Entities;


namespace BookSpark.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public void Add(Author author);

        IEnumerable<Author> GetAll();

        void Delete(int id);

        void Edit(Author author);

        Author Get(int id);
    }
}
