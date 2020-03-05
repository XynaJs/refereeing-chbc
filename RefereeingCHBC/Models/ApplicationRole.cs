using Microsoft.AspNetCore.Identity;

namespace RefereeingCHBC.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Access { get; set; }
    }
}