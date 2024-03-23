using BookSpark.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookSpark.Models.AuthorViewModels
{
    public class EditAuthorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Biography { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public EditAuthorViewModel() { }
        public EditAuthorViewModel(int id, string name, DateTime? birthdate, string? biography, ICollection<Book> books)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            Biography = biography;
            Books = books;
        }
    }
}
