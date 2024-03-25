using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
namespace Jovera.Areas.CRM.Pages.Configurations.ManageStore
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public Jovera.Models.Store store { get; set; }


        public List<Jovera.Models.Store> StoreList = new List<Jovera.Models.Store>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public Jovera.Models.Store storeObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            store = new Jovera.Models.Store();
            storeObj = new Jovera.Models.Store();
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void OnGet()
        {
            StoreList = _context.Stores.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleStoreForEdit(int StoreId)
        {
            store = _context.Stores.Where(c => c.StoreId == StoreId).FirstOrDefault();

            return new JsonResult(store);
        }

        public async Task<IActionResult> OnPostEditStore(int StoreId)
        {

            try
            {
                var model = _context.Stores.Where(c => c.StoreId == StoreId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Store Not Found");

                    return Redirect("/CRM/Configurations/ManageStore/Index");
                }



                model.StoreName = store.StoreName;
                model.Address = store.Address;
                model.Lat = store.Lat;
                model.Lng = store.Lng;


                var UpdatedStore = _context.Stores.Attach(model);

                UpdatedStore.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Store Edited successfully");

                return Redirect("/CRM/Configurations/ManageStore/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageStore/Index");
        }


        public IActionResult OnGetSingleStoreForView(int StoreId)
        {
            var Result = _context.Stores.Where(c => c.StoreId == StoreId).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleStoreForDelete(int StoreId)
        {
            store = _context.Stores.Where(c => c.StoreId == StoreId).FirstOrDefault();
            return new JsonResult(store);
        }

        public async Task<IActionResult> OnPostDeleteStore(int StoreId)
        {
            try
            {
                Jovera.Models.Store storeObj = _context.Stores.Where(e => e.StoreId == StoreId).FirstOrDefault();

                
                if (storeObj != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(storeObj.Email);
                    if (userExists != null)
                    {
                        await _userManager.DeleteAsync(userExists);

                    }
                
                    _context.Stores.Remove(storeObj);
                    
                    await _context.SaveChangesAsync();
                    

                    _toastNotification.AddSuccessToastMessage("Store Deleted Successfully");

                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Something went wrong Try Again");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("/CRM/Configurations/ManageStore/Index");
        }


        public async Task<IActionResult> OnPostAddStore()
        {

            try
            {
                if (store.Email != null)
                {
                    var userExists = await _userManager.FindByEmailAsync(store.Email);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                       
                    }
                    var user = new ApplicationUser { UserName = store.Email, Email = store.Email,EmailConfirmed=true};
                    var result = await _userManager.CreateAsync(user, store.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Store");
                        _context.Stores.Add(store);
                        _context.SaveChanges();
                        _toastNotification.AddSuccessToastMessage("Store Added Successfully");
                    }
                    return Redirect("/CRM/Configurations/ManageStore/Index");

                }

                

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/CRM/Configurations/ManageStore/Index");
        }

       

    }
}
