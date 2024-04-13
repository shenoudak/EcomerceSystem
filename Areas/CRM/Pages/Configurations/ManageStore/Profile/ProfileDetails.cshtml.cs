using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Jovera.Data;
using Jovera.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Jovera.ViewModels;
using Jovera.ViewModel;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageStore.Profile
{
    public class ProfileDetailsModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public List<string> storeCatagories { get; set; }
        public List<string> otherCatagories { get; set; }
        [BindProperty]
        public ChangePasswordVM changePasswordVM { get; set; }
        public Jovera.Models.Store storDetails { get; set; }
        public static int staticVendorId=0;
        public ProfileDetailsModel(CRMDBContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _signInManager = signInManager;
            _userManager = userManager;
            storDetails = new Jovera.Models.Store();
            _db = db;


        }
        public async Task<IActionResult> OnGet(int vendorId)
        {

            
            storDetails = _context.Stores.Include(e=>e.StoreProfileImages).Where(e => e.StoreId == vendorId).FirstOrDefault();

            if (storDetails == null)
            {
                return Redirect("/CRM/PageNotFound");
            }
            staticVendorId = vendorId;
            if (storDetails.CatagoriesTypes != null)
            {
                storeCatagories = storDetails.CatagoriesTypes.Split(",").ToList();


            }
            if (storDetails.OtherCatagories != null)
            {
                otherCatagories = storDetails.OtherCatagories.Split(",").ToList();


            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
           var storDetails = _context.Stores.Where(e => e.StoreId == staticVendorId).FirstOrDefault();
            if (storDetails == null)
            {
                return Redirect("/CRM/PageNotFound");
            }
            storDetails.StoreProfileStatusId = 3;
            var updatedStoreProfile = _context.Stores.Attach(storDetails);
            updatedStoreProfile.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Vendor Profile Status Updated Successfully");
            return Redirect("/CRM/Configurations/ManageStore/Index");
        

        }
        public async Task<IActionResult> OnPostUpdateStatusToReject(string RejectReason)
        {
            var storDetails = _context.Stores.Where(e => e.StoreId == staticVendorId).FirstOrDefault();
            if (storDetails == null)
            {
                return Redirect("/CRM/PageNotFound");
            }
            storDetails.StoreProfileStatusId = 4;
            storDetails.RejectProfileReason = RejectReason;
            var updatedStoreProfile = _context.Stores.Attach(storDetails);
            updatedStoreProfile.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Vendor Profile Status Updated Successfully");
            return Redirect("/CRM/Configurations/ManageStore/Index");


        }
    }
}
