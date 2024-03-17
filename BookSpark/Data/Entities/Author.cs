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

        public string? Bio { get; set; }
    }
}
