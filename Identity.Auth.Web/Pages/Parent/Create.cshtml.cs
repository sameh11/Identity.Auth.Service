using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models.Parent;
using Microsoft.AspNetCore.Identity;
using Identity.Auth.Web.Pages.Identity.Parent;

namespace Identity.Auth.Web._Pages_Identity_Parent
{
    public class CreateModel : UserPageModel
    {
        private readonly ParentDbContext _context;
        public UserManager<IdentityUser> UserManager { get; set; }
        public IdentityUser CurrentUser { get; set; } = default!;

        public CreateModel(ParentDbContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ParentProfile ParentProfile { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CurrentUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            ParentProfile.UserId= CurrentUser.Id;
            ParentProfile.Email= CurrentUser.Email;

            _context.ParentProfiles.Add(ParentProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
