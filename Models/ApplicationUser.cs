using Microsoft.AspNetCore.Identity;
namespace Jovera.Models
{
    #nullable disable
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfilePicture { get; set; }



    }
}