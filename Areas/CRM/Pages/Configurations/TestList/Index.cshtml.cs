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
using Jovera.DataTables;
using Microsoft.EntityFrameworkCore;
//using System.Drawing.Imaging;
namespace Jovera.Areas.CRM.Pages.Configurations.TestList
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
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

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
        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.Items.Count();

            var customersQuery = _context.Items.Select(c => new
            {
                ItemId = c.ItemId,
                ItemPrice = c.ItemPrice,
                ItemTitleAr = c.ItemTitleAr,
                ItemTitleEn = c.ItemTitleEn,
                ItemDescriptionAr = c.ItemDescriptionAr,
                ItemDescriptionEn = c.ItemDescriptionEn,
                IsActive = c.IsActive,
                OrderIndex = c.OrderIndex,
                BarCode = c.BarCode,
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.ItemTitleAr.ToUpper().Contains(searchText) ||
                    s.ItemTitleEn.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = await customersQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }
        public IActionResult OnGetSingleItemForEdit(int ItemId)
        {
            item = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

            return new JsonResult(item);
        }

        public async Task<IActionResult> OnPostEditItem(int ItemId, IFormFile Editfile)
        {

            try
            {
                var model = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Item Not Found");

                    return Redirect("/CRM/Configurations/ManageItem/Index");
                }


                if (Editfile != null)
                {


                    string folder = "images/Item/";

                    model.ItemImage = UploadImage(folder, Editfile);
                }
                else
                {
                    model.ItemImage = item.ItemImage;
                }

                model.ItemTitleAr = item.ItemTitleAr;
                model.ItemTitleEn = item.ItemTitleEn;
                model.IsActive = item.IsActive;
                model.OrderIndex = item.OrderIndex;
                model.ItemDescriptionAr = item.ItemDescriptionAr;
                model.ItemDescriptionEn = item.ItemDescriptionEn;
             


                var UpdatedItem = _context.Items.Attach(model);

                UpdatedItem.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Item Edited successfully");

                return Redirect("/CRM/Configurations/ManageItem/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageItem/Index");
        }


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
                Item ItemObj = _context.Items.Where(e => e.ItemId == ItemId).FirstOrDefault();


                if (ItemObj != null)
                {


                    _context.Items.Remove(itemObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Item Deleted successfully");

                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "/" + item.ItemImage);

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

            return Redirect("/CRM/Configurations/ManageItem/Index");
        }


        public async Task<IActionResult> OnPostAddItem(IFormFile file)
        {

            try
            {
                if (file != null)
                {
                    string folder = "Images/Item/";
                    item.ItemImage = UploadImage(folder, file);
                }
                item.CategoryId = 2;
                

                _context.Items.Add(item);
                _context.SaveChanges();
                GenerateBarCode(item.ItemId);
              

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
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
            QRCodeText = $"{this.Request.Scheme}://{this.Request.Host}/?Id=" + ItemId;
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
