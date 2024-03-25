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
    public class NewIndexModel : PageModel
    {
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;


        public List<Category> siteCategories { get; set; }
        public List<Item> Templates { get; set; }
        public int planPriceId { get; set; }
        public NewIndexModel(IToastNotification toastNotification, CRMDBContext context)
        {
          
            _toastNotification = toastNotification;
            _context = context;

            siteCategories = new List<Category>();
            Templates = new List<Item>();

        }

        public IActionResult OnGet(int? AffiliateId = null)
        {
            if (AffiliateId != null)
            {
                return Redirect($"/Register?AffiliateUser={AffiliateId}");
            }
            //locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            //BrowserCulture = locale.RequestCulture.UICulture.ToString();
            //siteCategories = _context.Categories.Where(e => e.IsActive == true).ToList();
            //var Template = _context.Items.Include(e => e.Category).Where(e => e.IsActive).ToList();
            //foreach (var template in Template)
            //{
            //    if (template != null)
            //    {

            //        Templates.Add(template);

            //    }

            //}

            return Page();
        }

    }
}
