using Identity.Auth.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.Auth.Web.Pages
{
    [Authorize]
    public class StoreModel : PageModel
    {
        public StoreModel(ProductDbContext ctx) => DbContext = ctx;
        public ProductDbContext DbContext { get; set; }
        public void OnGet()
        {
        }
    }
}
