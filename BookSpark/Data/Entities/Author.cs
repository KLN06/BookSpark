using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Biography { get; set; }

        public virtual ICollection<Book>? Books { get; set; }

        public Author(int id, string name, DateTime? birthdate, string? biography, ICollection<Book>? books)
            :this(id,name, birthdate, biography)
        {
            Books = books;
        }

        public Author(int id, string name, DateTime? birthdate, string? biography)
            : this(name, birthdate, biography)
        {
            Id = id;
        }

        public Author(string name, DateTime? birthdate, string? biography)
        {
            Name = name;
            Birthdate = birthdate;
            Biography = biography;
        }
    }
}
