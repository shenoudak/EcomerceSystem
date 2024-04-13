using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NToastNotify;
using System.Security.Policy;
using Microsoft.AspNetCore.Localization;

namespace Jovera.Pages
{
    public class ViewProductTestModel : PageModel
    {
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;
        public Item Product { get; set; }
        public ViewProductTestModel(IToastNotification toastNotification, CRMDBContext context)
        {

            _toastNotification = toastNotification;
            _context = context;

           

        }

        public IActionResult OnGet(int ProductId)
        {
            
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            Product = _context.Items.Include(e => e.MiniSubCategory).Include(e=>e.ItemImages).Where(e => e.IsActive == true&&e.IsDeleted==false).OrderBy(e => e.OrderIndex).FirstOrDefault();

            if (Product == null)
            {
                //redirect to page NOt Found

            }

            

            return Page();
        }


    }
}
