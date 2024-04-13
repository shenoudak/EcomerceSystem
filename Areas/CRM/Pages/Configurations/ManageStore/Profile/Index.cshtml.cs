using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Jovera.Data;
using Microsoft.EntityFrameworkCore;
using Jovera.Models;
using Microsoft.AspNetCore.Identity;
using Jovera.ViewModels;
namespace Jovera.Areas.CRM.Pages.Configurations.ManageStore.Profile
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public StoreProfileVM storeProfileVM { get; set; }
        public Jovera.Models.Store vendor { get; set; }
        public List<SubCategory> catagories { get; set; }
        public List<string> selectedStoreCatagories { get; set; }
        public List<SubCategory> selectedStoreSubcatgories { get; set; }
        public string url { get; set; }
        public IndexModel(CRMDBContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _signInManager = signInManager;
            _userManager = userManager;
            storeProfileVM = new StoreProfileVM();
            _db = db;
            catagories = new List<SubCategory>();

        }
        public async Task<IActionResult> OnGet(int vendorId)
        {
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            var user = await _userManager.GetUserAsync(User);
           
            vendor = _context.Stores.Include(e=>e.StoreProfileImages).Where(e => e.StoreId == vendorId).FirstOrDefault();
            if (vendor==null)
            {
                return Redirect("/CRM/PageNotFound");
            }
            storeProfileVM.StoreName = vendor.StoreName;
            storeProfileVM.StoreId = vendor.StoreId;
            storeProfileVM.StoreImage = vendor.StoreImage;
            storeProfileVM.TradeName = vendor.TradeName;
            storeProfileVM.ResponsibleForSupply = vendor.ResponsibleForSupply;
            storeProfileVM.Address = vendor.Address;
            storeProfileVM.Phone1 = vendor.Phone1;
            storeProfileVM.Phone2 = vendor.Phone2;
            storeProfileVM.AccountName = vendor.AccountName;
            storeProfileVM.TaxingNumber = vendor.TaxingNumber;
            storeProfileVM.AddingTax = vendor.AddingTax;
            storeProfileVM.BankName = vendor.BankName;
            storeProfileVM.IPan = vendor.IPan;
            storeProfileVM.IdPhoto = vendor.IdPhoto;
            storeProfileVM.LicensePhoto = vendor.LicensePhoto;
            storeProfileVM.Lat = vendor.Lat;
            storeProfileVM.Lng = vendor.Lng;
            if (vendor.OtherCatagories != null)
            {
                storeProfileVM.HasOtherCatgory = true;
            }
           

            storeProfileVM.OtherCatagory = vendor.OtherCatagories;
            if (vendor.CatagoriesTypes != null)
            {
                selectedStoreCatagories = vendor.CatagoriesTypes.Split(",").ToList();
                


            }
            
            catagories = _context.SubCategories.ToList();


            return Page();
        }

        public async Task<IActionResult> OnPost(IFormFile storeImage, IFormFile LicPhoto, IFormFile IdPhoto, IFormFileCollection ProductPhotos,List<string> states)
        {

            try
            {
                var Updatestore = _context.Stores.Where(e => e.StoreId == storeProfileVM.StoreId).FirstOrDefault();
                if (Updatestore == null)
                {
                    
                    return Redirect("/CRM/PageNotFound");
                }

                if (storeImage != null)
                {
                    string folder = "Images/Store/";
                    Updatestore.StoreImage = UploadImage(folder, storeImage);
                }
                if (LicPhoto != null)
                {
                    string folder = "Images/Store/";
                    Updatestore.LicensePhoto = UploadImage(folder, LicPhoto);
                }
                if (IdPhoto != null)
                {
                    string folder = "Images/Store/";
                    Updatestore.IdPhoto = UploadImage(folder, IdPhoto);
                }
                List<StoreProfileImage> storeProfileImages = new List<StoreProfileImage>();


                if (ProductPhotos.Count != 0)
                {
                    foreach (var item in ProductPhotos)
                    {
                        var itemImageObj = new StoreProfileImage();
                        string folder = "Images/Store/";
                        itemImageObj.ImageName = UploadImage(folder, item);
                        storeProfileImages.Add(itemImageObj);


                    }
                    Updatestore.StoreProfileImages = storeProfileImages;
                }
                if (states.Count > 0)
                {
                    storeProfileVM.CatagoriesTypes = states[0];
                    for(int i = 1; i < states.Count; i++)
                    {
                       
                        storeProfileVM.CatagoriesTypes += "," + states[i];
                    }

                }
                
             
                Updatestore.TradeName = storeProfileVM.TradeName;
                Updatestore.ResponsibleForSupply = storeProfileVM.ResponsibleForSupply;
                Updatestore.AddingTax = storeProfileVM.AddingTax;
                Updatestore.CatagoriesTypes = storeProfileVM.CatagoriesTypes;
                Updatestore.OtherCatagories = storeProfileVM.OtherCatagory;
                Updatestore.Address = storeProfileVM.Address;
                Updatestore.Lat = storeProfileVM.Lat;
                Updatestore.Lng = storeProfileVM.Lng;
                Updatestore.IPan = storeProfileVM.IPan;
                Updatestore.ShareRatio = storeProfileVM.ShareRatio;
                Updatestore.Phone1 = storeProfileVM.Phone1;
                Updatestore.Phone2 = storeProfileVM.Phone2;
                Updatestore.AccountName = storeProfileVM.AccountName;
                Updatestore.BankName = storeProfileVM.BankName;
                Updatestore.TaxingNumber = storeProfileVM.TaxingNumber;
                Updatestore.StoreProfileStatusId = 2;
                var updatedStoreProfile = _context.Stores.Attach(Updatestore);
                updatedStoreProfile.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();


                return Redirect("/CRM/Configurations/ManageStore/Index");


            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/CRM/Configurations/ManageStore/Index");
          
        }

        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

    }
}
