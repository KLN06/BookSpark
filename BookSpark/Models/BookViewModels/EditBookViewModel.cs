using BookSpark.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookSpark.Models.BookViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int PublishedYear { get; set; }

        public int GenreId { get; set; }

        public int AuthorId { get; set; }

        public string ImageLink { get; set; }

        public EditBookViewModel(string title, string description, int publishedYear, int genreId, int authorId, string imageLink)
        {
            Title = title;
            Description = description;
            PublishedYear = publishedYear;
            GenreId = genreId;
            AuthorId = authorId;
            ImageLink = imageLink;
        }

        public EditBookViewModel()
        { }
    }
}
