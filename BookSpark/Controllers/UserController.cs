using BookSpark.Data.Entities;
using BookSpark.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookSpark.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> SetAdminRole(string adminkey)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            if (adminkey == "gakal123")
            {
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SetUserRole()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            await userManager.AddToRoleAsync(user, Roles.User.ToString());
            await userManager.RemoveFromRoleAsync(user, Roles.Admin.ToString());

            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }
    }
}