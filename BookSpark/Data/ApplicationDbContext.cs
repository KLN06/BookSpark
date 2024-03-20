using BookSpark.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSpark.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
        
        public DbSet<Genre> Genres { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Wishlist> Wishlist { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { }
    }
}
