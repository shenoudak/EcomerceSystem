using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using NToastNotify;
using QRCoder;
using System.Drawing;
using System.Linq.Dynamic.Core;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
namespace Jovera.Areas.CRM.Pages.Configurations.ManageItem
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public string QRScan { get; set; }
        public string url { get; set; }
        public string QRCodeText { get; set; }

        [BindProperty]
        public Item item { get; set; }


        public List<Item> itemsList = new List<Item>();

        public Item itemObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            item = new Item();
            itemObj = new Item();
        }
        public void OnGet()
        {
            itemsList = _context.Items.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleItemForEdit(int ItemId)
        {
            item = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

            return new JsonResult(item);
        }

        //public async Task<IActionResult> OnPostEditItem(int ItemId, IFormFile Editfile)
        //{

        //    try
        //    {
        //        var model = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();
        //        if (model == null)
        //        {
        //            _toastNotification.AddErrorToastMessage("Item Not Found");

        //            return Redirect("/CRM/Configurations/ManageItem/Index");
        //        }


        //        if (Editfile != null)
        //        {


        //            string folder = "images/Item/";

        //            model.ItemImage = UploadImage(folder, Editfile);
        //        }
        //        else
        //        {
        //            model.ItemImage = item.ItemImage;
        //        }

        //        model.ItemTitleAr = item.ItemTitleAr;
        //        model.ItemTitleEn = item.ItemTitleEn;
        //        model.IsActive = item.IsActive;
        //        model.OrderIndex = item.OrderIndex;
        //        model.ItemDescriptionAr = item.ItemDescriptionAr;
        //        model.ItemDescriptionEn = item.ItemDescriptionEn;



        //        var UpdatedItem = _context.Items.Attach(model);

        //        UpdatedItem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        //        _context.SaveChanges();

        //        _toastNotification.AddSuccessToastMessage("Item Edited successfully");

        //        return Redirect("/CRM/Configurations/ManageItem/Index");

        //    }
        //    catch (Exception)
        //    {
        //        _toastNotification.AddErrorToastMessage("Something went Error");

        //    }
        //    return Redirect("/CRM/Configurations/ManageItem/Index");
        //}


        public IActionResult OnGetSingleItemForView(int ItemId)
        {
            var Result = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleItemForDelete(int ItemId)
        {
            item = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

            return new JsonResult(item);
        }

        public async Task<IActionResult> OnPostDeleteItem(int ItemId)
        {
            try
            {
                var NewItemObj = _context.Items.Where(e => e.ItemId == ItemId).FirstOrDefault();


                if (NewItemObj != null)
                {

                    DeleteSpecificItem(NewItemObj);
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("/CRM/Configurations/ManageItem/Index");
        }
        public IActionResult DeleteSpecificItem(Item item)
        {
            try
            {
                var itemsList = _context.ItemImages.Where(e => e.ItemId == item.ItemId).ToList();
                if (itemsList != null)
                {
                    _context.ItemImages.RemoveRange(itemsList);
                }

                _context.Items.Remove(itemObj);

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Item Deleted successfully");
            }

            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("/CRM/Configurations/ManageItem/Index");
        }


        public async Task<IActionResult> OnPostAddItem(IFormFile file, IFormFileCollection MorePhoto)
        {

            try
            {
                if (file != null)
                {
                    string folder = "Images/Item/";
                    item.ItemImage = UploadImage(folder, file);
                }
                List<ItemImage> itemImagesList = new List<ItemImage>();


                if (MorePhoto.Count != 0)
                {
                    foreach (var item in MorePhoto)
                    {
                        var itemImageObj = new ItemImage();
                        string folder = "Images/Item/";
                        itemImageObj.ImageName = UploadImage(folder, item);
                        itemImagesList.Add(itemImageObj);


                    }
                    item.ItemImages = itemImagesList;
                }
                item.PublishedDate = DateTime.Now;


                _context.Items.Add(item);
                _context.SaveChanges();
                if (!item.HasSubProduct)
                {
                    GenerateBarCode(item.ItemId);
                }



            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/CRM/Configurations/ManageItem/Index");
        }
        public async Task<IActionResult> OnPostEditItem(int ItemId, IFormFile Newfile, IFormFileCollection Editfilepond)
        {
            try
            {

                var DbItem = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

                if (DbItem == null)
                {
                    _toastNotification.AddErrorToastMessage("Item Not Found");

                    return Redirect("/CRM/Configurations/ManageItem/Index");
                }
                if (Newfile != null)
                {


                    string folder = "Images/Item/";

                    DbItem.ItemImage = UploadImage(folder, Newfile);
                }
                else
                {
                    DbItem.ItemImage = item.ItemImage;
                }

                List<ItemImage> EditItemImagesList = new List<ItemImage>();

                if (Editfilepond.Count != 0)
                {

                    var ImagesOfItem = _context.ItemImages.Where(i => i.ItemId == ItemId).ToList();
                    _context.RemoveRange(ImagesOfItem);

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

                DbItem.ItemTitleAr = item.ItemTitleAr;
                DbItem.ItemTitleEn = item.ItemTitleEn;
                DbItem.ItemDescriptionAr = item.ItemDescriptionAr;
                DbItem.ItemDescriptionEn = item.ItemDescriptionEn;
                DbItem.IsActive = item.IsActive;
                DbItem.OrderIndex = item.OrderIndex;
                DbItem.VideoUrl = item.VideoUrl;
                DbItem.ItemPrice = item.ItemPrice;
                DbItem.MiniSubCategoryId = item.MiniSubCategoryId;
                DbItem.HasSubProduct = item.HasSubProduct;
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


        public void GenerateBarCode(int ItemId)
        {
            var Event = _context.Items.Where(e => e.ItemId == ItemId).FirstOrDefault();
            if (Event != null)
            {
                Event.BarCode = ItemId.ToString();

            }
            QRCodeText = $"{this.Request.Scheme}://{this.Request.Host}/?Id=" + ItemId+ "&HasSubProd=" + false;
            string QRCodeName = QRCodeText;
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = BitmapToByteArray(QrBitmap);
            string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images/Item");
            string uniqePictureName = Guid.NewGuid() + ".jpeg";
            string uploadedImagePath = Path.Combine(uploadFolder, uniqePictureName);
            using (var imageFile = new FileStream(uploadedImagePath, FileMode.Create))
            {
                imageFile.Write(BitmapArray, 0, BitmapArray.Length);
                imageFile.Flush();
            }
            //nurseryMember.Banner = uniqePictureName;
            Event.BarCode = "Images/Item/" + uniqePictureName;
            var UpdatedEvent = _context.Items.Attach(Event);
            UpdatedEvent.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Item Added Successfully");



        }
        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
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
