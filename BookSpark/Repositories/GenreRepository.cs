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

        public IEnumerable<Genre> GetAll()
        {
            return context.Genres.ToList();
        }

        public Genre Get(int id)
        {
            return context.Genres.FirstOrDefault(genre => genre.Id == id);
        }
        public void Delete(int id)
        {
            var genre = Get(id);
            if(genre!=null)
            {
                context.Genres.Remove(genre);
                context.SaveChanges();
            }
        }

        public void Edit(Genre genre)
        {
            var entity = Get(genre.Id);
            entity.Name = genre.Name;

            context.SaveChanges();
        }
    }
}
