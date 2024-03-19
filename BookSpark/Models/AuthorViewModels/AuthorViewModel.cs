using BookSpark.Data.Entities;

namespace BookSpark.Models.AuthorViewModels
{
    public class AuthorViewModel
    {
        public AuthorViewModel(int id, string name, DateTime? birthdate, string? biography, ICollection<Book> books)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            Biography = biography;
            Books = books;            
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Biography { get; set; }

        public virtual ICollection<Book>? Books { get; set; }

        public string Title { get; set; }
    }
}
