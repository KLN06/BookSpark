using BookSpark.Data.Entities;


namespace BookSpark.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public void Add(Author author);

      // IEnumerable<Author> GetAll();
    }
}
