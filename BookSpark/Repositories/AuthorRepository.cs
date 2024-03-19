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
    }
}
