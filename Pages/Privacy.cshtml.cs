using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jovera.Data;
using Jovera.Models;

namespace Jovera.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly CRMDBContext _context;

        public PrivacyModel(CRMDBContext Context, ILogger<PrivacyModel> logger)
        {
            _context = Context;
            _logger = logger;
            _context = Context;
        }


        public async Task<IActionResult> OnGet()
        {

            return Page();
        }
    }
}
