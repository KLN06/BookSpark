﻿using BookSpark.Data.Entities;
using BookSpark.Models.BookViewModels;
using BookSpark.Repositories.Interfaces;
using BookSpark.Services.Interfaces;

namespace BookSpark.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }
        public void Add(int bookId)
        {
            wishlistRepository.Add(bookId);
        }

        public void Remove(int bookId)
        {
            wishlistRepository.Remove(bookId);
        }

        public async Task<IEnumerable<BookViewModel>> GetAll(string userId)
        {
            var books = await wishlistRepository.GetAll(userId);
            var bookModels = books.Select(book => new BookViewModel(book)).ToList();
            return bookModels;
        }


        public string GetUserId()
        {
            return wishlistRepository.GetUserId();
        }
    }
}