using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages.Identity.Parent
{
    [Authorize(Roles = "Parent")]
    public class UserPageModel : PageModel
    {
    }
}
