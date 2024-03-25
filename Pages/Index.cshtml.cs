using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Jovera.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet(int?AffiliateId=null)
        {
            if (AffiliateId != null)
            {
                return Redirect($"/Register?AffiliateUser={AffiliateId}");
            }
            return Page();
        }

    }
}

