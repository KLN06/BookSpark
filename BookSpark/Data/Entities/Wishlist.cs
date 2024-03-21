using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Wishlist
    {
        [Key]
        public string Id { get; set; }

        public List<Book>? Books { get; set;}

        public Wishlist(string id)
        {
            Id = id;
            Books = new List<Book>();
        }
    }
}
