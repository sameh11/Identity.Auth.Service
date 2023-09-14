using Identity.Auth.Web.Pages.Identity.Parent;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages.Identity
{
    public class ManageModel : UserPageModel
    {
        public ManageModel(UserManager<IdentityUser> userMgr)
            => UserManager = userMgr;
        public UserManager<IdentityUser> UserManager { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public async Task OnGetAsync()
        {
            IdentityUser CurrentUser = await UserManager.GetUserAsync(User);
            Email = CurrentUser?.Email ?? "(No Value)";
            Phone = CurrentUser?.PhoneNumber ?? "(No Value)";
        }
    }
}
