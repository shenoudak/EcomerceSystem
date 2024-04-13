using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Jovera.DataTables;
using NToastNotify;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Jovera.Areas.CRM.Pages.Configurations.ManageSubProduct.ManageStepTwo
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }

        [BindProperty]
        public Jovera.Models.Size addStepTwo { get; set; }
        public Jovera.Models.Size addStepTwoObj { get; set; }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            addStepTwo = new Jovera.Models.Size();
            addStepTwoObj = new Jovera.Models.Size();
        }
        public void OnGet()
        {

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.Sizes.Where(e => e.IsDeleted == false).Count();

            var customersQuery = _context.Sizes.Where(e => e.IsDeleted == false).Select(i => new
            {
                SizeId = i.SizeId,
                SizeTLAR = i.SizeTLAR,
                SizeTLEN = i.SizeTLEN,

            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.SizeTLAR.ToUpper().Contains(searchText) ||
                    s.SizeTLEN.ToUpper().Contains(searchText)
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

        public async Task<IActionResult> OnPostAddStepTwo()
        {

            try
            {
                _context.Sizes.Add(addStepTwo);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Added Successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/crm/configurations/Managesubproduct/managestepTwo/index");
        }
        public IActionResult OnGetSingleStepTwoForEdit(int SizeId)
        {
            addStepTwo = _context.Sizes.Where(c => c.SizeId == SizeId).FirstOrDefault();

            return new JsonResult(addStepTwo);
        }
        public async Task<IActionResult> OnPostEditStepTwo(int SizeId)
        {

            try
            {
                var model = _context.Sizes.Where(c => c.SizeId == SizeId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/crm/configurations/Managesubproduct/managestepTwo/index");
                }
                model.SizeTLAR = addStepTwo.SizeTLAR;
                model.SizeTLEN = addStepTwo.SizeTLEN;
                var UpdatedStepTwo = _context.Sizes.Attach(model);

                UpdatedStepTwo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Edited Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/crm/configurations/Managesubproduct/managestepTwo/index");
        }
        public IActionResult OnGetSingleStepTwoForView(int SizeId)
        {
            var Result = _context.Sizes.Where(c => c.SizeId == SizeId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleStepTwoForDelete(int SizeId)
        {
            var Result = _context.Sizes.Where(c => c.SizeId == SizeId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public async Task<IActionResult> OnPostDeleteStepTwo(int SizeId)
        {
            try
            {
                var model = _context.Sizes.Where(c => c.SizeId == SizeId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/crm/configurations/Managesubproduct/managestepTwo/index");
                }
                model.IsDeleted = true;
                var UpdatedStepTwo = _context.Sizes.Attach(model);

                UpdatedStepTwo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Step Deleted Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }

            return Redirect("/crm/configurations/Managesubproduct/managestepTwo/index");

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
