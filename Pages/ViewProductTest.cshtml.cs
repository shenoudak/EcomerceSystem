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
using System.Linq.Expressions;
using Jovera.ViewModels;

namespace Jovera.Pages
{
    public class ViewProductTestModel : PageModel
    {
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;
        public Item Product { get; set; }
        public static int ProId { get; set; }
        public string concatItemId = "";
        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty]
        public ProductReview addProductReview { get; set; }
        [BindProperty]
        public AddSubProductToCartVm addSubProductToCartVm { get; set; }
        public List<ProductReview>productReviews { get; set; }
        public List<SubProductStepOne> subProductStepOnes = new List<SubProductStepOne>();
        public int ReviewCount = 0;
        public ViewProductTestModel(IToastNotification toastNotification, CRMDBContext context, UserManager<ApplicationUser> userManager)
        {

            _toastNotification = toastNotification;
            _context = context;
            _userManager = userManager;
            addProductReview = new ProductReview();
            addSubProductToCartVm = new AddSubProductToCartVm();
            productReviews = new List<ProductReview>();


		}

        public IActionResult OnGet(int ProductId)
        {
            
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            Product = _context.Items.Include(e => e.MiniSubCategory).Include(e=>e.ItemImages).Where(e => e.IsActive == true&&e.IsDeleted==false).OrderBy(e => e.OrderIndex).FirstOrDefault();

            if (Product == null)
            {
                Redirect("/error");

            }
            if (Product.HasSubProduct)
            {
                subProductStepOnes = _context.SubProductStepOnes.Include(e=>e.StepOne).Where(e => e.ItemId == ProductId && e.IsDeleted == false).ToList();

			}
            ProId = Product.ItemId;
            concatItemId = "item" + Product.ItemId;
            productReviews = _context.ProductReviews.Where(e => e.ItemId == ProductId).Take(4).ToList();
            ReviewCount = productReviews.Count();
           




			return Page();
        }
        public IActionResult OnGetSingleAllStepTwo(int stepOneId)
        {
            var cities = _context.MiniSubProducts.Include(e=>e.StepTwo).Where(e => e.SubProductStepOneId == stepOneId).Select(e => new
            {
                StepTwoTLAR = e.StepTwo.StepTwoTLAR,
                StepTwoTLEN = e.StepTwo.StepTwoTLEN,
                MiniSubProductId = e.MiniSubProductId,
                Quantity = e.Quantity,
                
            }).ToList();
            return new JsonResult(cities);
        }
        public async Task<IActionResult> OnPost()
        {
            try{
                var prod = _context.Items.Where(e => e.ItemId == ProId).FirstOrDefault();
                if (prod == null)
                {
                    Redirect("/error");
                }
				//var user = await _userManager.GetUserAsync(User);
				//if (user == null)
				//{
				//    Redirect("/Login");
				//}
				//var customer = await _context.Customers.Where(e => e.CustomerEmail == user.Email).FirstOrDefaultAsync();
				//if (customer == null)
				//{
				//    Redirect("/Login");
				//}
				var customer =  _context.Customers.FirstOrDefault();
				addProductReview.ItemId =ProId;
				addProductReview.CustomerId =customer.CustomerId;
				_context.ProductReviews.Add(addProductReview);
                _context.SaveChanges();

            }
            catch(Exception ex)
            {

            }
            
          
            return Redirect($"/ViewProductTestModel?ProductId={ProId}");
        }


    }
}
