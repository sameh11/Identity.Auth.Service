using Identity.Auth.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages
{
    public class LandingModel : PageModel
    {
        public LandingModel(ProductDbContext ctx) => DbContext = ctx;
        public ProductDbContext DbContext { get; set; }
        public void OnGet()
        {
        }
    }
}
