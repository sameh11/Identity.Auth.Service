using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Parent
{
    public class ParentDbContext : DbContext
    {
        public ParentDbContext(DbContextOptions<ParentDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParentProfile> ParentProfiles { get; set; }
        public DbSet<ChildProfile> ChildProfiles { get; set; }



    }
}
