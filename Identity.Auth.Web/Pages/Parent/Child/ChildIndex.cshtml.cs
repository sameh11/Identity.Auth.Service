using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;
using Microsoft.AspNetCore.Identity;

namespace Identity.Auth.Web.Pages.Identity.Parent.Child
{
    public class ChildIndexModel : UserPageModel
    {
        private readonly ParentDbContext _context;
        public IList<ChildProfile> ChildProfile { get;set; } = default!;
        public ParentProfile Parent { get;set; } = default!;
        public UserManager<IdentityUser> UserManager { get; set; }

        public ChildIndexModel(ParentDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        private async Task<ParentProfile> LoadAsync()
        {
            var CurrentUser = await UserManager.FindByEmailAsync(User.Identity.Name);

            return _context.ParentProfiles.Where(x => x.UserId == CurrentUser.Id).FirstOrDefault();
        }


        public async Task OnGetAsync()
        {
            var profile = await LoadAsync();
            Parent = _context.ParentProfiles.Where(x => x.UserId == profile.UserId).First();
            var children = _context.ChildProfiles.Where(x => x.ParentID == Parent.Id.ToString()).ToList();
            if (children != null && children.Any())
            {
                ChildProfile = await _context.ChildProfiles.ToListAsync();
            }
        }
    }
}
