using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static IdentityServer4.Models.IdentityResources;

namespace Domain.Models.Parent
{
    public class ParentProfile : Profile
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? FatherName { get; set; }
        [MaxLength(100)] 
        public string? MotherName { get; set; }
        public string? FatherOccupation { get; set; }
        public string? MotherOccupation { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }

        public virtual List<ChildProfile> Children { get; set; }

    }
}
