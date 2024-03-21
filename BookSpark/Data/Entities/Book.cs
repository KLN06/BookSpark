using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSpark.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int PublishedYear { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        public string ImageLink { get; set; }

        public Book()
        { }

        public Book(string title, string description, int publishedYear, int genreId, int authorId, string imageLink)
        {
            Title = title;
            Description = description;
            PublishedYear = publishedYear;
            GenreId = genreId;
            AuthorId = authorId;
            ImageLink = imageLink;
        }
        public Book() { }
    }
}
