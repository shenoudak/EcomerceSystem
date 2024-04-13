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

        public ProductController(CRMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, int? catagoriyId = null, string? query = null,int?customerId=null)
        {

            //var items = _context.Items.Include(e => e.MiniSubCategory).ToList();
            //if (catagoriyId!=null)
            //{
            //    items = items.Where(e => e.MiniSubCategoryId == catagoriyId.Value).ToList();
            //}
            //if (query != null)
            //{
            //    items = items.Where(e => e.MiniSubCategory.MiniSubCategoryTLEN.Contains(query)||e.MiniSubCategory.MiniSubCategoryTLAR.Contains(query)).ToList();
            //}
            //items = items.Select(e => new
            //{
            //    ItemTitleAr= e.ItemTitleAr
            //}).ToList();
            var items = _context.Items.Include(e => e.MiniSubCategory).Select(i => new
            {
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
                isFav=_context.ProductFavourites.Any(o => o.ItemId == i.ItemId && o.CustomerId == customerId)

            });

            if (catagoriyId != 0)
            {
                items = items.Where(e => e.MiniSubCategoryId == catagoriyId.Value);
            }

            if (query != null)
            {
                items = items.Where(e => e.MiniSubCategory.MiniSubCategoryTLEN.Contains(query) || e.MiniSubCategory.MiniSubCategoryTLAR.Contains(query));
            }
            //items = items.AsQueryable();
            //if (catagoriyId != null)
            //{
            //    items = items.Where(e => e.MiniSubCategoryId == catagoriyId).ToList();

            //}



            return Json(await DataSourceLoader.LoadAsync(items, loadOptions));
        }


    }
}