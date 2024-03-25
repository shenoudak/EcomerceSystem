using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using System.IO;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageCatagory
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public Category category { get; set; }


        public List<Category> categoriesList = new List<Category>();

        public Category categoryObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            category = new Category();
            categoryObj = new Category();
        }
        public void OnGet()
        {
            categoriesList = _context.Categories.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleCategoryForEdit(int CategoryId)
        {
            category = _context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();

            return new JsonResult(category);
        }

        public async Task<IActionResult> OnPostEditCategory(int CategoryId, IFormFile Editfile)
        {
         
            try
            {
                var model = _context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Category Not Found");

                    return Redirect("/CRM/Configurations/ManageCatagory/Index");
                }


                if (Editfile != null)
                {


                    string folder = "images/Category/";

                    model.CategoryPic = UploadImage(folder, Editfile);
                }
                else
                {
                    model.CategoryPic = category.CategoryPic;
                }

                model.CategoryTLAR = category.CategoryTLAR;
                model.CategoryTLEN = category.CategoryTLEN;
                model.IsActive = category.IsActive;
                model.OrderIndex = category.OrderIndex;
               

                var UpdatedCategory = _context.Categories.Attach(model);

                UpdatedCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Category Edited successfully");

                return Redirect("/CRM/Configurations/ManageCatagory/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageCatagory/Index");
        }


        public IActionResult OnGetSingleCategoryForView(int CategoryId)
        {
            var Result = _context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleCategoryForDelete(int CategoryId)
        {
            category = _context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault();
            return new JsonResult(category);
        }

        public async Task<IActionResult> OnPostDeleteCategory(int CategoryId)
        {
            try
            {
                Category CatObj = _context.Categories.Where(e => e.CategoryId == CategoryId).FirstOrDefault();


                if (CatObj != null)
                {


                    _context.Categories.Remove(CatObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Category Deleted successfully");

                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "/" + category.CategoryPic);

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

            return Redirect("/CRM/Configurations/ManageCatagory/Index");
        }


        public async Task<IActionResult> OnPostAddCategory(IFormFile file)
        {
           
            try
            {
                if (file != null)
                {
                    string folder = "Images/Category/";
                    category.CategoryPic = UploadImage(folder, file);
                }

                _context.Categories.Add(category);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Category Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/CRM/Configurations/ManageCatagory/Index");
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
