using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using Microsoft.EntityFrameworkCore;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageSubCatagory
{

    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public static int CatId { get; set; }

        public string url { get; set; }


        [BindProperty]
        public SubCategory subCategory { get; set; }


        public List<SubCategory> SubCategoriesList = new List<SubCategory>();

        public SubCategory subCategoryObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            subCategory = new SubCategory();
            subCategoryObj = new SubCategory();
        }
      
        public void OnGet(int CatagoryId)
        {
            if (CatagoryId != 0)
            {
                subCategory.CategoryId = CatagoryId;
                CatId = CatagoryId;
            }
            SubCategoriesList = _context.SubCategories.Include(e=>e.Category).Where(e => e.CategoryId == CatagoryId).ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }


        public IActionResult OnGetSingleSubCategoryForEdit(int SubCategoryId)
        {
            subCategory = _context.SubCategories.Where(c => c.SubCategoryId == SubCategoryId).FirstOrDefault();

            return new JsonResult(subCategory);
        }

        public async Task<IActionResult> OnPostEditSubCategory(int SubCategoryId)
        {

            try
            {
                var model = _context.SubCategories.Where(c => c.SubCategoryId == SubCategoryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("SubCategory Not Found");

                    return Redirect($"/CRM/Configurations/ManageSubCatagory/Index?CatagoryId={CatId}");

                }
                model.SubCategoryTLAR = subCategory.SubCategoryTLAR;
                model.SubCategoryTLEN = subCategory.SubCategoryTLEN;
                model.IsActive = subCategory.IsActive;
                model.OrderIndex = subCategory.OrderIndex;
                var UpdatedSubCategory = _context.SubCategories.Attach(model);

                UpdatedSubCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("SubCategory Edited successfully");
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/CRM/Configurations/ManageSubCatagory/Index?CatagoryId={CatId}");

        }


        public IActionResult OnGetSingleSubCategoryForView(int SubCategoryId)
        {
            var Result = _context.SubCategories.Include(e=>e.Category).Where(c => c.SubCategoryId == SubCategoryId).Select(e => new
            {
                SubCategoryId = e.SubCategoryId,
                SubCategoryTLAR = e.SubCategoryTLAR,
                SubCategoryTLEN = e.SubCategoryTLEN,
                CategoryTLAR = e.Category.CategoryTLAR,
                CategoryTLEN = e.Category.CategoryTLEN,
                OrderIndex = e.OrderIndex,
                IsActive = e.IsActive
            }).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleSubCategoryForDelete(int SubCategoryId)
        {
            subCategory = _context.SubCategories.Where(c => c.SubCategoryId == SubCategoryId).FirstOrDefault();
            return new JsonResult(subCategory);
        }

        public async Task<IActionResult> OnPostDeleteSubCategory(int SubCategoryId)
        {
            try
            {
                SubCategory SubCatObj = _context.SubCategories.Where(e => e.SubCategoryId == SubCategoryId).FirstOrDefault();


                if (SubCatObj != null)
                {


                    _context.SubCategories.Remove(SubCatObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("SubCategory Deleted successfully");

                   
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

            return Redirect($"/CRM/Configurations/ManageSubCatagory/Index?CatagoryId={CatId}");

        }


        public async Task<IActionResult> OnPostAddSubCategory()
        {

            try
            {
               
                _context.SubCategories.Add(subCategory);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("SubCategory Added Successfully");
                //CatId = subCategory.CategoryId;

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect($"/CRM/Configurations/ManageSubCatagory/Index?CatagoryId={CatId}");
        }

      

    }
}
