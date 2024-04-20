using Jovera.Data;
using Jovera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Jovera.ViewModels;

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
        public static int prodId { get; set; }
        [BindProperty]
        public Item ItemDetails { get; set; }
        public int ItemReviews = 0;
        
        public int ItemReviewSum = 0;
        public int ItemReviewCount = 0;
        public int AvailableQuantityInStore = 0;
        public List<MiniSubProductVm> miniSubProductsList = new List<MiniSubProductVm>();
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
            ItemDetails = _context.Items.Include(e => e.ItemImages).Include(e => e.ItemStatus).Include(e=>e.MiniSubCategory).Include(e => e.SubProducts).ThenInclude(e=>e.StepOne).ThenInclude(e=>e.StepTwos).FirstOrDefault(a => a.ItemId == ItemId);
            if (ItemDetails == null)
            {
                Redirect("/CRM/PageNotFound");
            }
            prodId = ItemId;
                if (ItemDetails.HasSubProduct)
            {
                AvailableQuantityInStore = _context.MiniSubProducts.Where(e=>e.IsDeleted==false).Include(e=>e.SubProductStepOne).Where(e => e.SubProductStepOne.ItemId == ItemId).Sum(e => e.Quantity);
               
                miniSubProductsList = _context.MiniSubProducts.Include(e => e.StepTwo).Include(e => e.SubProductStepOne).ThenInclude(e => e.StepOne).Where(e => e.SubProductStepOne.ItemId == ItemId && e.IsDeleted == false).Select(i => new MiniSubProductVm
                {
                    ItemQRCode = i.ItemQRCode,
                    StepOneTLAR = i.SubProductStepOne.StepOne.StepOneTLAR,
                    StepOneTLEN = i.SubProductStepOne.StepOne.StepOneTLEN,
                    StepTwoTLAR = i.StepTwo.StepTwoTLAR,
                    StepTwoTLEN = i.StepTwo.StepTwoTLEN,
                    Quantity = i.Quantity,
                }).ToList();

            }
            else
            {
                AvailableQuantityInStore = ItemDetails.Quantity;

			}

            var itemReviewsList = _context.ProductReviews.Where(e => e.ItemId == ItemId).ToList();
            ItemReviewCount = itemReviewsList.Count();
            ItemReviewSum = itemReviewsList.Sum(e => e.Rating);
            if (ItemReviewCount != 0)
            {
                ItemReviews = ItemReviewSum / ItemReviewCount;

            }

            return Page();
        }
        public IActionResult OnGetSingleUpdateItemToActivateForEdit(int ItemId)
        {
            var item = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

            return new JsonResult(item);
        }
        public async Task<IActionResult> OnPostActivateItem(int ItemId)
        {

            try
            {
                var model = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Item Not Found");
                    return Page();

                }




                model.DiscountRatio = ItemDetails.DiscountRatio;
                model.ItemPrice = ItemDetails.ItemPrice;
                model.IsActive = true;
                model.ItemStatusId = 3;
                double oldPriceOfItem = ItemDetails.ItemPrice+((ItemDetails.ItemPrice * ItemDetails.DiscountRatio) / 100);
                model.OldPrice = oldPriceOfItem;
                var UpdatedItem = _context.Items.Attach(model);

                UpdatedItem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Item Edited successfully");

                return Redirect($"/CRM/Configurations/ManageItem/ProductDetails?ItemId={ItemId}");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/CRM/Configurations/ManageItem/ProductDetails?ItemId={ItemDetails.ItemId}");
        }
        public async Task<IActionResult> OnPostEditItemStatusToReject(string RejectReason)
        {
            int vendorId = 0;
            try
            {
                var ItemDetailsExist = _context.Items.Where(e=>e.ItemId== prodId).FirstOrDefault();

                //= _context.Items.Where(e => e.ItemId == staticItemId).FirstOrDefault();
                if (ItemDetails == null)
                {
                    return Redirect("/CRM/PageNotFound");
                }
                vendorId = ItemDetailsExist.StoreId.Value;
                ItemDetailsExist.ItemStatusId = 4;
                ItemDetailsExist.IsActive = false;
                ItemDetailsExist.RejectReason = RejectReason;
                var updatedItemStatus = _context.Items.Attach(ItemDetailsExist);
                updatedItemStatus.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Item Status Updated Successfully");

            }
            catch(Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Something Went Error ..Try Again");
            }
            return Redirect($"/CRM/Configurations/ManageItem/ProductDetails?ItemId={prodId}");


        }
    }
}
