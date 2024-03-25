using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jovera.Data;
using Jovera.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;

namespace Jovera.Areas.Customer.Pages
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        public int CustomerCount { get; set; }
        public int OrderCount { get; set; }
        public int WithdrawRequestsCount { get; set; }
        public double SumOrderAmount { get; set; }
        public double ProfitAmount { get; set; }
        public double CurrentBalance { get; set; }
        public string Url { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(CRMDBContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult>OnGet()
        {
            
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Login");
            }

            var customer =await _context.Customers.Where(e => e.CustomerEmail == user.Email).FirstOrDefaultAsync();
            if (customer==null)
            {
                return Redirect("/Login");
            }
             var PassCustomerId = customer.CustomerId;
            
             Url = $"{this.Request.Scheme}://{this.Request.Host}?AffiliateId={PassCustomerId}";
             CustomerCount = _context.Customers.Where(e=>e.AffiliateId== customer.CustomerId).Count();
             OrderCount = 0;
             SumOrderAmount =0;
             ProfitAmount =0;
             CurrentBalance = 0;
             WithdrawRequestsCount = 0;
            //NewAppointmentCount = _context.Appointments.Where(e => e.AppointmentStatusId == 1).Count();
            //NewAppointmentAmount = _context.Appointments.Where(e => e.AppointmentStatusId == 1).Sum(e => e.TotalAmount.Value);
            //CanceledAppointmentCount = _context.Appointments.Where(e => e.AppointmentStatusId == 2).Count();
            //CanceledAppointmentAmount = _context.Appointments.Where(e => e.AppointmentStatusId == 2).Sum(e => e.TotalAmount.Value);
            //ClosedAppointmentCount = _context.Appointments.Where(e => e.AppointmentStatusId == 3).Count();
            //ClosedAppointmentAmount = _context.Appointments.Where(e => e.AppointmentStatusId == 3).Sum(e => e.TotalAmount.Value);
            return Page();
        }

    }
}
