using Jovera.Data;
using Jovera.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageItem
{
    public class EditItemModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public string url { get; set; }
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public Item EditItem { get; set; }


        public EditItemModel(CRMDBContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
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
            EditItem = _context.Items.Include(e=>e.ItemImages).FirstOrDefault(a => a.ItemId == ItemId);
            return Page();
        }
        public async Task<IActionResult> OnPost(int ItemId, IFormFile file, IFormFileCollection Editfilepond)
        {
            try
            {

                var DbItem = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

                if (DbItem == null)
                {
                    _toastNotification.AddErrorToastMessage("Item Not Found");

                    return Redirect("/CRM/Configurations/ManageItem/Index");
                }
                if (file != null)
                {


                    string folder = "Images/Item/";

                    DbItem.ItemImage = UploadImage(folder, file);
                }
                else
                {
                    DbItem.ItemImage = EditItem.ItemImage;
                }

                List<ItemImage> EditItemImagesList = new List<ItemImage>();

                if (Editfilepond.Count != 0)
                {

                    

                    foreach (var item in Editfilepond)
                    {

                        var itemImageObj = new ItemImage();
                        string folder = "Images/Item/";
                        itemImageObj.ImageName = UploadImage(folder, item);
                        itemImageObj.ItemId = ItemId;
                        EditItemImagesList.Add(itemImageObj);


                    }
                    _context.ItemImages.AddRange(EditItemImagesList);
                }

                DbItem.ItemTitleAr = EditItem.ItemTitleAr;
                DbItem.ItemTitleEn = EditItem.ItemTitleEn;
                DbItem.ItemDescriptionAr = EditItem.ItemDescriptionAr;
                DbItem.ItemDescriptionEn = EditItem.ItemDescriptionEn;
                DbItem.IsActive = EditItem.IsActive;
                DbItem.OrderIndex = EditItem.OrderIndex;
                DbItem.VideoUrl = EditItem.VideoUrl;
                DbItem.ItemPrice = EditItem.ItemPrice;
                DbItem.MiniSubCategoryId = EditItem.MiniSubCategoryId;
                DbItem.HasSubProduct = EditItem.HasSubProduct;
                var UpdatedItem = _context.Items.Attach(DbItem);
                UpdatedItem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Item Edited successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageItem/Index");
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
