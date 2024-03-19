using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

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
    }
}
