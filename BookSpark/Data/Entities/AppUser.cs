using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSpark.Data.Entities
{
    public class AppUser : IdentityUser
    {

        [ForeignKey("Wishlist")]
        public string WishlistId { get; set; }

        public Wishlist? Wishlist { get; set; }

        public AppUser(string id, string email, string username, string wishListId)
        {
            Id = id;
            Email = email;
            UserName = username;
            WishlistId = wishListId;
        }

        public AppUser()
        {
        }
    }
}
