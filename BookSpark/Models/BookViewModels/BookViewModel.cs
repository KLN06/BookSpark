using BookSpark.Data.Entities;

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
            Title = book.Title;
            Description = book.Description;
            PublishedYear = book.PublishedYear;
            GenreId = book.GenreId;
            GenreName = book.Genre.Name;
            AuthorId = book.AuthorId;
            AuthorName = book.Author.Name;
            ImageLink = book.ImageLink;
        }
    }
}
