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
        public Jovera.Models.StepOne addStepOne { get; set; }
        public Jovera.Models.StepOne addStepOneObj { get; set; }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            _userManager = userManager;
            addStepOne = new Jovera.Models.StepOne();
            addStepOneObj = new Jovera.Models.StepOne();
        }
        public void OnGet()
        {
           
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }



        public async Task<JsonResult> OnPostAsync()
        {


            var recordsTotal = _context.StepOnes.Where(e=>e.IsDeleted==false).Count();

            var customersQuery = _context.StepOnes.Where(e => e.IsDeleted == false).Select(i => new
            {
                StepOneId = i.StepOneId,
                StepOneTLAR = i.StepOneTLAR,
                StepOneTLEN = i.StepOneTLEN,
               
            }).AsQueryable();


            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.StepOneTLAR.ToUpper().Contains(searchText) ||
                    s.StepOneTLEN.ToUpper().Contains(searchText)
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
                _context.StepOnes.Add(addStepOne);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Added Successfully");
            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
        }
        public IActionResult OnGetSingleStepOneForEdit(int StepOneId)
        {
            addStepOne = _context.StepOnes.Where(c => c.StepOneId == StepOneId).FirstOrDefault();

            return new JsonResult(addStepOne);
        }
        public async Task<IActionResult> OnPostEditStepOne(int StepOneId)
        {

            try
            {
                var model = _context.StepOnes.Where(c => c.StepOneId == StepOneId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
                }
                model.StepOneTLAR = addStepOne.StepOneTLAR;
                model.StepOneTLEN = addStepOne.StepOneTLEN;
                var UpdatedStepOne = _context.StepOnes.Attach(model);

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
        public IActionResult OnGetSingleStepOneForView(int StepOneId)
        {
            var Result = _context.StepOnes.Where(c => c.StepOneId == StepOneId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleStepOneForDelete(int StepOneId)
        {
            var Result = _context.StepOnes.Where(c => c.StepOneId == StepOneId).FirstOrDefault();
            return new JsonResult(Result);
        }
        public async Task<IActionResult> OnPostDeleteStepOne(int StepOneId)
        {
            try
            {
                var model = _context.StepOnes.Where(c => c.StepOneId == StepOneId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Object Not Found");

                    return Redirect("/crm/configurations/Managesubproduct/managestepone/index");
                }
                model.IsDeleted = true;
                var UpdatedStepOne = _context.StepOnes.Attach(model);

                UpdatedStepOne.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Step Deleted Successfully");



            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }

            return Redirect("/crm/configurations/Managesubproduct/managestepone/index");

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
