using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;
using Identity.Auth.Web.Pages.Identity.Parent;

namespace Identity.Auth.Web._Pages_Identity_Parent
{
    public class EditModel : UserPageModel
    {
        private readonly Domain.Models.Parent.ParentDbContext _context;

        public EditModel(Domain.Models.Parent.ParentDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParentProfile ParentProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ParentProfiles == null)
            {
                return NotFound();
            }

            var parentprofile =  await _context.ParentProfiles.FirstOrDefaultAsync(m => m.Id == id);
            if (parentprofile == null)
            {
                return NotFound();
            }
            ParentProfile = parentprofile;
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

            _context.Attach(ParentProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentProfileExists(ParentProfile.Id))
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

        private bool ParentProfileExists(int id)
        {
          return _context.ParentProfiles.Any(e => e.Id == id);
        }
    }
}
