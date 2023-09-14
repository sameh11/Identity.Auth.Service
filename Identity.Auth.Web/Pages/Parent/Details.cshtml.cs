using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;
using Identity.Auth.Web.Pages.Identity.Parent;

namespace Identity.Auth.Web._Pages_Identity_Parent
{
    public class DetailsModel : UserPageModel
    {
        private readonly Domain.Models.Parent.ParentDbContext _context;

        public DetailsModel(Domain.Models.Parent.ParentDbContext context)
        {
            _context = context;
        }

      public ParentProfile ParentProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ParentProfiles == null)
            {
                return NotFound();
            }

            var parentprofile = await _context.ParentProfiles.FirstOrDefaultAsync(m => m.Id == id);
            if (parentprofile == null)
            {
                return NotFound();
            }
            else 
            {
                ParentProfile = parentprofile;
            }
            return Page();
        }
    }
}
