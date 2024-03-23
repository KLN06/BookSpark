using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

      //  public ICollection<Book> Books { get; set; }
        public Genre()
        { }
        public Genre(int id, string name)
            : this(name)
        {
            Id = id;
        }
        public Genre(string name)
        {
            Name = name;
        }
        public override bool Equals(object? other)
            => Equals((Genre)other);

        public bool Equals(Genre other)
            => other != null &&
            Id == other.Id &&
            Name == other.Name;

        /*
        public Genre(int id, string name, ICollection<Book> books) : this(id, name)
        {
            Books = books;
        }*/
    }
}
