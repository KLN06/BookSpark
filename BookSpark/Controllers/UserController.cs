using BookSpark.Data.Entities;
using BookSpark.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookSpark.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        /*public async void SetRole(string adminkey)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            if (adminkey == "gakal123")
            {
                userManager.AddToRoleAsync(user, Roles.Admin.ToString()); 
            }
        }*/
    }
}
