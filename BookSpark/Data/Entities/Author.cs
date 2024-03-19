using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? Biography { get; set; }

        public Author()
        {

        }
        public Author(string name, DateOnly? birthdate, string? biography)
        {
            Name = name;
            Birthdate = birthdate;
            Biography = biography;
        }
    }
}
