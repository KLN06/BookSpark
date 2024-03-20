using BookSpark.Data.Entities;
using BookSpark.Repositories;

namespace BookSpark.Models.BookViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PublishedYear { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string ImageLink { get; set; }

        public BookViewModel(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Description = book.Description;
            PublishedYear = book.PublishedYear;
            GenreId = book.GenreId;
            AuthorId = book.AuthorId;
            ImageLink = book.ImageLink;
        }
    }
}
