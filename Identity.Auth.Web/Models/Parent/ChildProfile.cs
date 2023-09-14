using System.Reflection;

namespace Domain.Models.Parent
{
    public class ChildProfile
    {
        public string Id { get; set; }
        public string ChildName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string ParentID { get; set; }
    }
}