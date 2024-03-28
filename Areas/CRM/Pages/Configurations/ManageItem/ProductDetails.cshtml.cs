using Jovera.Data;
using Jovera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageItem
{
    public class ProductDetailsModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Item ItemDetails { get; set; }


        public ProductDetailsModel(CRMDBContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
            _toastNotification = toastNotification;
        }


        public IActionResult OnGet(int ItemId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            ItemDetails = _context.Items.Include(e => e.ItemImages).Include(e=>e.MiniSubCategory).FirstOrDefault(a => a.ItemId == ItemId);
            return Page();
        }
       
    }
}
