using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.Parent.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(new JsonResult(from c in User.Claims select new { c.Type, c.Value }));
        }
    }
}
