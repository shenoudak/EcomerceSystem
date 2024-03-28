using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using System.IO;
using Microsoft.EntityFrameworkCore;
namespace Jovera.Areas.CRM.Pages.Configurations.MiniSubCatagory
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public static int CatId { get; set; }

        public string url { get; set; }


        [BindProperty]
        public MiniSubCategory miniSubCategory { get; set; }


        public List<MiniSubCategory> categoriesList = new List<MiniSubCategory>();

        public MiniSubCategory categoryObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            miniSubCategory = new MiniSubCategory();
            categoryObj = new MiniSubCategory();
        }
        public void OnGet(int SubCatagoryId)
        {
            if (SubCatagoryId != 0)
            {
                miniSubCategory.SubCategoryId = SubCatagoryId;
                CatId = SubCatagoryId;
            }
            categoriesList = _context.MiniSubCategories.Include(e => e.SubCategory).Where(e => e.SubCategoryId == SubCatagoryId).ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleCategoryForEdit(int MiniSubCategoryId)
        {
            miniSubCategory = _context.MiniSubCategories.Where(c => c.MiniSubCategoryId == MiniSubCategoryId).FirstOrDefault();

            return new JsonResult(miniSubCategory);
        }

        public async Task<IActionResult> OnPostEditCategory(int MiniSubCategoryId, IFormFile Editfile)
        {

            try
            {
                var model = _context.MiniSubCategories.Where(c => c.MiniSubCategoryId == MiniSubCategoryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Category Not Found");

                    return Redirect($"/CRM/Configurations/MiniSubCatagory/Index?SubCatagoryId={CatId}");
                }


                if (Editfile != null)
                {


                    string folder = "images/Category/";

                    model.MiniSubCategoryPic = UploadImage(folder, Editfile);
                }
                else
                {
                    model.MiniSubCategoryPic = miniSubCategory.MiniSubCategoryPic;
                }

                model.MiniSubCategoryTLAR = miniSubCategory.MiniSubCategoryTLAR;
                model.MiniSubCategoryTLEN = miniSubCategory.MiniSubCategoryTLEN;
                model.IsActive = miniSubCategory.IsActive;
                model.OrderIndex = miniSubCategory.OrderIndex;


                var UpdatedMiniSubCategory = _context.MiniSubCategories.Attach(model);

                UpdatedMiniSubCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Mini SubCategory Edited successfully");

                return Redirect($"/CRM/Configurations/MiniSubCatagory/Index?SubCatagoryId={CatId}");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect($"/CRM/Configurations/MiniSubCatagory/Index?SubCatagoryId={CatId}");
        }


        public IActionResult OnGetSingleCategoryForView(int MiniSubCategoryId)
        {
            var Result = _context.MiniSubCategories.Where(c => c.MiniSubCategoryId == MiniSubCategoryId).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleCategoryForDelete(int MiniSubCategoryId)
        {
            miniSubCategory = _context.MiniSubCategories.Where(c => c.MiniSubCategoryId == MiniSubCategoryId).FirstOrDefault();
            return new JsonResult(miniSubCategory);
        }

        public async Task<IActionResult> OnPostDeleteCategory(int MiniSubCategoryId)
        {
            try
            {
                MiniSubCategory CatObj = _context.MiniSubCategories.Where(e => e.MiniSubCategoryId == MiniSubCategoryId).FirstOrDefault();


                if (CatObj != null)
                {


                    _context.MiniSubCategories.Remove(CatObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Mini SubCategory Deleted successfully");

                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "/" + miniSubCategory.MiniSubCategoryPic);

                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
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

            return Redirect($"/CRM/Configurations/MiniSubCatagory/Index?SubCatagoryId={CatId}");
        }


        public async Task<IActionResult> OnPostAddCategory(IFormFile file)
        {

            try
            {
                if (file != null)
                {
                    string folder = "Images/Category/";
                    miniSubCategory.MiniSubCategoryPic = UploadImage(folder, file);
                }

                _context.MiniSubCategories.Add(miniSubCategory);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Mini SubCategory Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect($"/CRM/Configurations/MiniSubCatagory/Index?SubCatagoryId={CatId}");
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
