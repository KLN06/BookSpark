using BookSpark.Data.Entities;
using System.Data.Entity;

namespace BookSpark.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
        
        public DbSet<Genre> Genres { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Wishlist> Wishlist { get; set; }
    }
}
