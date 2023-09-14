using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;

namespace Identity.Auth.Web.Pages.Identity.Parent.Child
{
    public class ChildEditModel : UserPageModel
    {
        private readonly Domain.Models.Parent.ParentDbContext _context;

        public ChildEditModel(Domain.Models.Parent.ParentDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChildProfile ChildProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.ChildProfiles == null)
            {
                return NotFound();
            }

            var childprofile =  await _context.ChildProfiles.FirstOrDefaultAsync(m => m.Id == id);
            if (childprofile == null)
            {
                return NotFound();
            }
            ChildProfile = childprofile;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ChildProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChildProfileExists(ChildProfile.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ChildProfileExists(string id)
        {
          return _context.ChildProfiles.Any(e => e.Id == id);
        }
    }
}
