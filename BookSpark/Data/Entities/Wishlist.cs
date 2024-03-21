using System.ComponentModel.DataAnnotations;

namespace BookSpark.Data.Entities
{
    public class Wishlist
    {
        [Key]
        public string Id { get; set; }

        public ICollection<Book>? Books { get; set;}

        public Wishlist(string id)
        {
            Id = id;
            Books = new List<Book>();
        }
/*
        public Wishlist(string id, ICollection<Book> books)
        {
            Id = id;
            Books = books;
        }*/

        public Wishlist() { }
    }
}
