using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Models.Parent;
using Microsoft.AspNetCore.Identity;

namespace Identity.Auth.Web.Pages.Identity.Parent.Child
{
    public class ChildCreateModel : UserPageModel
    {
        private readonly ParentDbContext _context;
        public UserManager<IdentityUser> UserManager { get; set; }

        private ParentProfile ParentProfile { get; set; } = default!;

        public ChildCreateModel(ParentDbContext context, 
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
        public ChildProfile ChildProfile { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var CurrentUser = await UserManager.FindByEmailAsync(User.Identity.Name);
            ChildProfile.ParentID= CurrentUser.Id;
            _context.ChildProfiles.Add(ChildProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
