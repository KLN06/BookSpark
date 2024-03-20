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
            //return context.Authors.Include("Books").ToList();
            return context.Authors.Include("Books").ToList();
        }

        public Author Get(int id)
        {
            return context.Authors.FirstOrDefault(author => author.Id == id);
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
