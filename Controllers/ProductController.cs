using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Jovera.Data;
using Jovera.Models;
using Jovera.ViewModels;

namespace Jovera.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private CRMDBContext _context;

        public ProductController(CRMDBContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, int? catagoriyId = null, string? query = null) {
            

            var items = _context.Items.Include(e=>e.MiniSubCategory).Where(e=>e.MiniSubCategoryId==catagoriyId).Select(i => new {
                i.MiniSubCategory.MiniSubCategoryTLAR,
                i.MiniSubCategory.MiniSubCategoryTLEN,
                i.MiniSubCategory,
                i.MiniSubCategoryId,
                i.ItemId,
                i.ItemImage,
                i.ItemTitleAr,
                i.ItemTitleEn,
                i.OldPrice,
                i.SellingPriceForCustomer,
                i.OurSellingPrice,
                i.ItemPrice,
              
            });
            //if (catagoriyId != null)
            //{
            //    items = items.Where(e => e.MiniSubCategoryId == catagoriyId).ToList();

            //}

           

            return Json(await DataSourceLoader.LoadAsync(items, loadOptions));
        }

      
    }
}