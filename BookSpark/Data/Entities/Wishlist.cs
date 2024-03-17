using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Book> Books { get; set;}
    }
}
