using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages.Identity.Admin
{
    [Authorize(Roles = "Dashboard")]
    public class AdminPageModel : PageModel
    {
    }
}
