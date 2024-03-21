using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSpark.Data.Entities
{
    public class Wishlist
    {
        [Key]
        public string Id { get; set; }

        public ICollection<Book>? Books { get; set;}

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public Wishlist()
        { 
        }
        public Wishlist(string userAppId, AppUser appuser, string wishlistId)
        {
            Id = wishlistId;
            AppUser = appuser;
            AppUserId = userAppId;
            Books = new List<Book>();
        }
/*
        public Wishlist(string id, ICollection<Book> books)
        {
            Id = id;
            Books = books;
        }*/
    }
}
