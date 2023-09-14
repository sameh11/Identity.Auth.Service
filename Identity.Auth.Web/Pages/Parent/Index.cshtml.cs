using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Parent;
using Identity.Auth.Web.Pages.Identity.Parent;
using Microsoft.AspNetCore.Identity;
using static IdentityServer4.Models.IdentityResources;
using AutoMapper.Features;
using System.Reflection;

namespace Identity.Auth.Web._Pages_Identity_Parent
{
    public class IndexModel : UserPageModel
    {
        private readonly ParentDbContext _context;
        public ParentProfile ParentProfile { get; set; } = default!;
        public UserManager<IdentityUser> UserManager { get; set; }
        public IdentityUser CurrentUser { get; set; } = default!;
        public IEnumerable<(string, string)> ProfileProps { get; set; }

        public IndexModel(Domain.Models.Parent.ParentDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }
        private async Task<ParentProfile> LoadAsync()
        {
            CurrentUser = await UserManager.FindByEmailAsync(User.Identity.Name);

            return _context.ParentProfiles.Where(x => x.UserId == CurrentUser.Id).FirstOrDefault();
        }


        public async Task OnGetAsync()
        {
            var profile = await LoadAsync();
            if (profile is not null)
            {
                ParentProfile = profile;
                ProfileProps = ParentProfile.GetType()
                    .GetProperties()
                    .Select(prop => (prop.Name, prop.GetValue(ParentProfile)?
                    .ToString() ?? ""));
            }
            else
            {
                ParentProfile = new ParentProfile()
                {
                    UserId = CurrentUser.Id,
                    Email = CurrentUser.Email,
                };
            }
        }
        //private object? TryGetValue(PropertyInfo property, object obj)
        //{
        //    property.SetValue(obj, "", null);
        //    //object defaultValue = type.IsValueType ? Activator.CreateInstance(type) : null;
        //}
    }
}
