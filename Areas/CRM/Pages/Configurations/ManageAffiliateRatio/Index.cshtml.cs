using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using Microsoft.AspNetCore.Identity;
namespace Jovera.Areas.CRM.Pages.Configurations.ManageAffiliateRatio
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public AffiliateRatio affiliateRatio { get; set; }


        public List<AffiliateRatio> AffiliateRatioList = new List<AffiliateRatio>();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AffiliateRatio AffiliateRatioObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager,
                                            IToastNotification toastNotification, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            affiliateRatio = new AffiliateRatio();
            AffiliateRatioObj = new AffiliateRatio();
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void OnGet()
        {
            AffiliateRatioList = _context.AffiliateRatios.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleAffiliateRatioForEdit(int AffiliateRatioId)
        {
            affiliateRatio = _context.AffiliateRatios.Where(c => c.AffiliateRatioId == AffiliateRatioId).FirstOrDefault();

            return new JsonResult(affiliateRatio);
        }

        public async Task<IActionResult> OnPostEditAffiliateRatio(int AffiliateRatioId)
        {

            try
            {
                var model = _context.AffiliateRatios.Where(c => c.AffiliateRatioId == AffiliateRatioId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Affiliate Ratio Not Found");

                    return Redirect("/CRM/Configurations/ManageAffiliateRatio/Index");
                }



                model.ratioInPrecentage = affiliateRatio.ratioInPrecentage;
               

                var UpdatedAffiliateRatio = _context.AffiliateRatios.Attach(model);

                UpdatedAffiliateRatio.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Affiliate Ratio Edited successfully");

                return Redirect("/CRM/Configurations/ManageAffiliateRatio/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageAffiliateRatio/Index");
        }



       

    }
}
