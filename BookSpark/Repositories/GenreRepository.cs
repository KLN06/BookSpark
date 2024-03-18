using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Repositories.Interfaces;

namespace BookSpark.Repositories
{ 
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext context;

        public GenreRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Genre genre)
        {
            context.Genres.Add(genre);
            context.SaveChanges();
        }
    }
}
