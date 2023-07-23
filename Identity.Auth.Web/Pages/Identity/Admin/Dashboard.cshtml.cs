using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages.Identity.Admin
{
    public class DashboardModel : PageModel
    {
        public DashboardModel(UserManager<IdentityUser> userMgr) => UserManager = userMgr;
        public UserManager<IdentityUser> UserManager { get; set; }
        public int UsersCount { get; set; } = 0;
        public int UsersUnconfirmed { get; set; } = 0;
        public int UsersLockedout { get; set; } = 0;
        public int UsersTwoFactor { get; set; } = 0;
        private readonly string[] emails = {
            "alice@example.com", "bob@example.com", "charlie@example.com"
        };
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (IdentityUser existingUser in UserManager.Users.ToList())
            {
                IdentityResult result = await UserManager.DeleteAsync(existingUser);
                result.Process(ModelState);
            }
            foreach (string email in emails)
            {
                IdentityUser userObject = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                await UserManager.CreateAsync(userObject);
                IdentityResult result = await UserManager.CreateAsync(userObject);
                result.Process(ModelState);
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage();
            }
            return Page();
        }
        public void OnGet()
        {
            UsersCount = UserManager.Users.Count();
        }
    }
}
