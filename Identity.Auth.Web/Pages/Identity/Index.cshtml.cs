using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages.Identity
{
    public class IndexModel : UserPageModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public void OnGet()
        {
        }
    }
}
