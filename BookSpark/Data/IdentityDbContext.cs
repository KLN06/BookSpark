using Microsoft.EntityFrameworkCore;

namespace BookSpark.Data
{
    public class IdentityDbContext
    {
        private DbContextOptions<ApplicationDbContext> options;

        public IdentityDbContext(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }
    }
}