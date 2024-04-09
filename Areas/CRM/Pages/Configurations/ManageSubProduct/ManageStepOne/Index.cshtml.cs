using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Jovera.DataTables;
using NToastNotify;
using System.Linq.Dynamic.Core;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageSubProduct.ManageStepOne
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }

        [BindProperty]
        public Jovera.Models.Color addStepOne { get; set; }
        public Jovera.Models.Color addStepOneObj { get; set; }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            addStepOne = new Jovera.Models.Color();
            addStepOneObj = new Jovera.Models.Color();
        }
        public void OnGet()
        {
           
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.Colors.Count();

            var customersQuery = _context.Colors.Select(i => new
            {
                ColorId = i.ColorId,
                ColorTLAR = i.ColorTLAR,
                ColorTLEN = i.ColorTLEN,
               
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.ColorTLAR.ToUpper().Contains(searchText) ||
                    s.ColorTLEN.ToUpper().Contains(searchText)
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

        public async Task<IActionResult> OnPostAddStepOne()
        {

            try
            {
                _context.Colors.Add(addStepOne);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Added Successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
        }
        public IActionResult OnGetSingleStepOneForEdit(int ColorId)
        {
            addStepOne = _context.Colors.Where(c => c.ColorId == ColorId).FirstOrDefault();

            return new JsonResult(addStepOne);
        }
        public async Task<IActionResult> OnPostEditStepOne(int ColorId)
        {

            try
            {
                var model = _context.Colors.Where(c => c.ColorId == ColorId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
                }
                model.ColorTLAR = addStepOne.ColorTLAR;
                model.ColorTLEN = addStepOne.ColorTLEN;
                var UpdatedStepOne = _context.Colors.Attach(model);

                UpdatedStepOne.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");

               

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
        }
        public IActionResult OnGetSingleStepOneForView(int ColorId)
        {
            var Result = _context.Colors.Where(c => c.ColorId == ColorId).FirstOrDefault();
            return new JsonResult(Result);
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
