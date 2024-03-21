using BookSpark.Data;
using BookSpark.Data.Entities;
using BookSpark.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookSpark.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext context;

        public AuthorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Authors.Include("Books").ToList();
        }

        public Author Get(int id)
        {
            var author = context.Authors.Include("Books").FirstOrDefault(author => author.Id == id);
            if(author is null)
            {
                throw new ArgumentException("The author cannot be null");
            }
            return author;
        }
        public void Delete(int id)
        {
            var author = Get(id);
            if (author != null)
            {
                context.Authors.Remove(author);
                context.SaveChanges();
            }
        }

        public void Edit(Author author)
        {
            var entity = Get(author.Id);
            entity.Name = author.Name;
            entity.Birthdate = author.Birthdate;
            entity.Biography = author.Biography;
            context.SaveChanges();
        }
    }
}
