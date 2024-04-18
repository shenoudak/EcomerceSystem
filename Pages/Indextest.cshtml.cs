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
    public class IndextestModel : PageModel
    {
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;
        [BindProperty]
        public int CustomerId { get; set; }

        public List<MiniSubCategory> siteCategories { get; set; }
        public List<Item> Templates { get; set; }
        public List<Item> Products { get; set; }
        public int planPriceId { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public IndextestModel(IToastNotification toastNotification, CRMDBContext context, UserManager<ApplicationUser> userManager)
        {

            _toastNotification = toastNotification;
            _context = context;

            siteCategories = new List<MiniSubCategory>();
            Templates = new List<Item>();
            Products = new List<Item>();
            _userManager = userManager;

        }

        public async Task<IActionResult> OnGet(int? AffiliateId = null, int? catagoriyId = null, string? query = null)
        {
            if (AffiliateId != null)
            {
                return Redirect($"/Register?AffiliateUser={AffiliateId}");
            }
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            Products = _context.Items.Include(e => e.MiniSubCategory).Where(e => e.IsActive == true).OrderBy(e => e.OrderIndex).ToList();
            //var customer = await _context.Customers.FirstOrDefaultAsync();
            //CustomerId = customer.CustomerId;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var customer = await _context.Customers.Where(e => e.CustomerEmail == user.Email).FirstOrDefaultAsync();
                if (customer != null)
                {
                    CustomerId = customer.CustomerId;
                }
            }


            return Page();
        }
        public async Task<IActionResult> OnGetSingleAddItemToFavourite(int ItemId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return new JsonResult(new { IsLogin = false, IsSuccess = false, ItemExist = false });
                }
                var customer = await _context.Customers.Where(e => e.CustomerEmail == user.Email).FirstOrDefaultAsync();
                if (customer == null)
                {
                    return new JsonResult(new { IsLogin = false, IsSuccess = false, ItemExist = false,InFav = false });
                }
                //var customer = await _context.Customers.FirstOrDefaultAsync();
                //if (customer == null)
                //{
                //    return new JsonResult(new { IsLogin = false, IsSuccess = false, ItemExist = false, InFav = false });
                //}
                var product = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();
                if (product == null)
                {
                    return new JsonResult(new { IsLogin = true, IsSuccess = false, ItemExist = false, InFav = false });
                }
                var checkItemInFavourite = _context.ProductFavourites.Where(e => e.CustomerId == customer.CustomerId && e.ItemId == ItemId).FirstOrDefault();
                if (checkItemInFavourite!=null)
                {
                    _context.ProductFavourites.Remove(checkItemInFavourite);
                    _context.SaveChanges();
                    return new JsonResult(new { IsLogin = true, IsSuccess = true, ItemExist = true,InFav=true });
                }
                else
                {
                    var AddProductFavourite = new ProductFavourite()
                    {
                        ItemId = ItemId,
                        CustomerId = customer.CustomerId,

                    };
                    _context.ProductFavourites.Add(AddProductFavourite);
                    _context.SaveChanges();
                    return new JsonResult(new { IsLogin = true, IsSuccess = true, ItemExist = true , InFav = false });
                }
               
                
               

            }
            catch (Exception ex)
            {
                return new JsonResult(new { IsLogin = true, IsSuccess = false, ItemExist = true });
            }
            

            
        }

    }
}

