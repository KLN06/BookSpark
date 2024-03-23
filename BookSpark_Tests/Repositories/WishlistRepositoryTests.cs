using BookSpark.Data.Entities;
using BookSpark.Data;
using BookSpark.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using BookSpark.Repositories.Interfaces;
using System.Diagnostics;

namespace BookSpark_Tests.Repositories
{
    public class WishlistRepositoryTests 
    {
        private WishlistRepository wishlistRepository;
        private ApplicationDbContext context;
        private string userId;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            context = new ApplicationDbContext(options);
            wishlistRepository = new WishlistRepository(context);
            userId = Guid.NewGuid().ToString();
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        #region Add

        [Test]
        public async Task GivenAnExistingWishlistAndNoBook_WhenAddingABookToWishlist_AddsIt()
        {
            var user = new AppUser();
            var wishlist = new Wishlist(userId, user, Guid.NewGuid().ToString());
            var book = SeedBooks().FirstOrDefault();
            context.Users.Add(user);
            context.SaveChanges();

            await wishlistRepository.Add(book.Id, userId);

            var result = context.Wishlist.Include(w => w.Books).LastOrDefault();
            Assert.IsNotNull(result, "The book was not to the wishlist");
            Assert.AreEqual(1, result.Books.Count, "The book has not been added once");
            Assert.AreEqual(book.Id, result.Books.First().Id, "Ids do not match");
        }

        [Test]
        public async Task GivenANonExistingWishlist_WhenAddingABookToWishlist_AddsIt()
        {
            var user = new AppUser();
            context.Users.Add(user);
            context.SaveChanges();
            var book = SeedBooks().FirstOrDefault();

            await wishlistRepository.Add(book.Id, userId);

            var result = context.Wishlist.Include(w => w.Books).LastOrDefault();
            Assert.IsNotNull(result, "The wishlist was not created");
            Assert.AreEqual(userId, result.AppUserId, "User IDs do not match");
            Assert.AreEqual(1, result.Books.Count, "The book has not been added once");
            Assert.AreEqual(book.Id, result.Books.First().Id, "Ids do not match");
        }

        #endregion

        #region Remove
        [Test]
        public async Task GivenAnExistingBookInWishlist_WhenRemovingBook_RemovesIt()
        {
            var user = new AppUser();
            context.Users.Add(user);
            var wishlist = new Wishlist(user.Id, user, Guid.NewGuid().ToString());
            var book = SeedBooks().FirstOrDefault();
            wishlist.Books.Add(book);
            context.Wishlist.Add(wishlist);
            context.SaveChanges();

            // Ensure that GetWishlist returns a valid Wishlist
            Assert.IsNotNull(await wishlistRepository.GetWishlist(user.Id), "The wishlist does not exist");

            // Remove the book
            await wishlistRepository.Remove(book.Id, user.Id);

            var result = context.Wishlist.Include(w => w.Books).LastOrDefault();
            Assert.AreEqual(0, result.Books.Count, "The book was not removed");
        }

        [Test]
        public async Task GivenNonExistingWishlist_WhenRemovingBook_RemovesIt()
        {
            var user = new AppUser();
            context.Users.Add(user);
            context.SaveChanges();

            Assert.ThrowsAsync<ArgumentException>(async () => await wishlistRepository.Remove(1, user.Id));
        }

        [Test]
        public async Task GivenAnNonExistingBookInWishlist_WhenRemovingBook_RemovesIt()
        {
            var user = new AppUser();
            context.Users.Add(user);
            var wishlist = new Wishlist(user.Id, user, Guid.NewGuid().ToString()); context.Wishlist.Include("AppUser").Include("Books");
            context.Wishlist.Add(wishlist);
            context.SaveChanges();

            // Ensure that GetWishlist returns a valid Wishlist
            Assert.IsNotNull(await wishlistRepository.GetWishlist(user.Id), "The wishlist does not exist");

            Assert.ThrowsAsync<ArgumentException>(async () => await wishlistRepository.Remove(-1, user.Id));
        }

        #endregion

        #region GetWishlist
        [Test]
        public async Task GivenAnExistingWishlist_WhenGettingWishlist_ReturnsWishlist()
        {
            var user = new AppUser();
            context.Users.Add(user);
            var wishlist = new Wishlist(userId, user, Guid.NewGuid().ToString());
            context.Wishlist.Include("AppUser").Include("Books");
            context.Wishlist.Add(wishlist);
            context.SaveChanges();

            var retrievedWishlist = await wishlistRepository.GetWishlist(user.Id);

            Assert.IsNotNull(retrievedWishlist, "Wishlist not found");
            Assert.AreEqual(wishlist.Id, retrievedWishlist.Id, "Wishlist IDs do not match");
            Assert.AreEqual(wishlist.AppUserId, retrievedWishlist.AppUserId, "User IDs do not match");
        }

        [Test]
        public async Task GivenAnNonExistingWishlist_WhenGettingAll_ReturnsNull()
        {
            var retrievedWishlist = await wishlistRepository.GetWishlist(userId);

            Assert.IsNull(retrievedWishlist, "Wishlist found unexpectedly");
        }

        #endregion

        #region GetAll
        [Test]
        public async Task GivenAnValidUserId_WhenGettingAll_ReturnsListOfBooksInWishlist()
        {
            var user = new AppUser();
            context.Users.Add(user);
            var wishlist = new Wishlist(user.Id, user, Guid.NewGuid().ToString());
            var book1 = SeedBooks().First();
            var book2 = SeedBooks().Last();
            wishlist.Books.Add(book1);
            wishlist.Books.Add(book2);
            context.Wishlist.Add(wishlist);
            context.SaveChanges();

            // Ensure that GetWishlist returns a valid Wishlist
            Assert.IsNotNull(await wishlistRepository.GetWishlist(user.Id), "The wishlist does not exist");

            var result = await wishlistRepository.GetAll(user.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.Title == book1.Title));
            Assert.IsTrue(result.Any(b => b.Title == book2.Title));
        }

        [Test]
        public async Task GivenInvalidUserId_WhenGettingAll_ReturnsEmptyList()
        {
            string userId = "nonExistentUserId";

            var result = await wishlistRepository.GetAll(userId);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        #endregion

        private IEnumerable<Book> SeedBooks()
        {
            var books = new[]
            {
                 new Book()
                 {
                    Title = "Title1",
                    Description = "Description",
                    PublishedYear = 2000,
                    Genre = new Genre()
                    {
                        Name = "GenreName"
                    },
                    GenreId = 1,
                    Author = new Author()
                    {
                        Name = "AuthorName",
                        Birthdate = DateTime.Now,
                        Biography = "bio"
                    },
                    AuthorId = 1,
                    ImageLink = "image"
                 },
                  new Book()
                 {
                    Title = "Title1",
                    Description = "Description",
                    PublishedYear = 2000,
                    Genre = new Genre()
                    {
                        Name = "GenreName"
                    },
                    GenreId = 1,
                    Author = new Author()
                    {
                        Name = "AuthorName",
                        Birthdate = DateTime.Now,
                        Biography = "bio"
                    },
                    AuthorId = 1,
                    ImageLink = "image"
                 }
            };

            context.Books.AddRange(books);
            context.SaveChanges();

            return books;
        }
    }

}
