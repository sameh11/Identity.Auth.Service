using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;

namespace Identity.Auth.Web.Pages.Identity.Parent.Child
{
    public class ChildDeleteModel : UserPageModel
    {
        private readonly Domain.Models.Parent.ParentDbContext _context;

        public ChildDeleteModel(Domain.Models.Parent.ParentDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public ChildProfile ChildProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.ChildProfiles == null)
            {
                return NotFound();
            }

            var childprofile = await _context.ChildProfiles.FirstOrDefaultAsync(m => m.Id == id);

            if (childprofile == null)
            {
                return NotFound();
            }
            else 
            {
                ChildProfile = childprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.ChildProfiles == null)
            {
                return NotFound();
            }
            var childprofile = await _context.ChildProfiles.FindAsync(id);

            if (childprofile != null)
            {
                ChildProfile = childprofile;
                _context.ChildProfiles.Remove(ChildProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
