using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Jovera.DataTables;
using NToastNotify;
using QRCoder;
using System.Drawing;
using System.Linq.Dynamic.Core;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageItem
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;

        public string QRScan { get; set; }
        public static int staticVendorId { get; set; }
        public string url { get; set; }
        public string QRCodeText { get; set; }

        [BindProperty]
        public Item item { get; set; }


        public List<Item> itemsList = new List<Item>();

        public Item itemObj { get; set; }
        [BindProperty]
        public Jovera.Models.Store Vendor { get; set; }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            item = new Item();
            itemObj = new Item();
        }
        public IActionResult OnGet(int vendorId)
        {
            Vendor = _context.Stores.Where(e => e.StoreId == vendorId).FirstOrDefault();
            if (Vendor == null)
            {
                return Redirect("/CRM/PageNotFound");
            }
            staticVendorId = Vendor.StoreId;
            itemsList = _context.Items.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
            return Page();
        }

        public async Task<IActionResult> OnGetSingleItemForEditAsync(int ItemId)
        {
           
            
            item = _context.Items.Where(c => c.ItemId == ItemId).FirstOrDefault();

            return new JsonResult(item);
        }

        public async Task<JsonResult> OnPostAsync()
        {
            

            var recordsTotal = _context.Items.Where(e=>e.StoreId== staticVendorId).Count();

            var customersQuery = _context.Items.Where(e => e.StoreId == staticVendorId).Select(i => new
            {
                ItemId = i.ItemId,
                ItemImage = i.ItemImage,
                ItemTitleAr = i.ItemTitleAr,
                ItemTitleEn = i.ItemTitleEn,
                OurSellingPrice = i.OurSellingPrice,
                SellingPriceForCustomer = i.SellingPriceForCustomer,
                ItemPrice = i.ItemPrice,
                OrderIndex = i.OrderIndex,
                IsActive = i.IsActive,
                HasSubProduct = i.HasSubProduct,
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


        
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

    }
}
