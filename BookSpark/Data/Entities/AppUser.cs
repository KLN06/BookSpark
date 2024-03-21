using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSpark.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser(string id, string email, string username)
        {
            Id = id;
            Email = email;
            UserName = username;
        }

        public AppUser()
        {
        }
    }
}
