using BookSpark.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookSpark.Models.BookViewModels
{
    public class AddBookViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int PublishedYear { get; set; }

        public int GenreId { get; set; }

        public int AuthorId { get; set; }

        public string ImageLink { get; set; }

    }
}
