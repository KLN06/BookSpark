/*using BookSpark.Data.Entities;
using BookSpark.Data;
using BookSpark.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSpark_Tests.Repositories
{
    public class WishlistRepositoryTests
    {
        private WishlistRepository wishlistRepository;
        private ApplicationDbContext context;

        private IEnumerable<Book> SeedProducts()
        {
            var books = new[]
            {
                new Book("Title", "description", "2024", "1", "1", "imageLink"),
                new Book("Title", "description", "2024", "1", "1", "imageLink"),
                new Book("Title", "description", "2024", "1", "1", "imageLink")
            };

            applicationContext.Products.AddRange(products);
            applicationContext.SaveChanges();

            return products;
        }

        private ApplicationContext SetUpApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("UnitTestsDb");

            return new ApplicationContext(options.Options);
        }

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            wishlistRepository = new WishlistRepository(context, null, null);
        }

        [TearDown]
        public void TearDown()
        {
            applicationContext.Database.EnsureDeleted();
            applicationContext.Dispose();
        }

        [Test]
        public async Task Add_WithValidBookId_AddsBookToWishlist()
        {
            var userId = "user123";
            var bookId = 1;
            var user = new AppUser { Id = userId };
            var wishlist = new Wishlist(userId, user, Guid.NewGuid().ToString());
            var book = new Book { Id = bookId };
            context.Users.Add(user);
            context.Books.Add(book);
            context.SaveChanges();

            await wishlistRepository.Add(bookId);

            var result = context.Wishlist.Include(w => w.Books).FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Books.Count);
            Assert.AreEqual(bookId, result.Books.First().Id);
        }
    }
}
*/