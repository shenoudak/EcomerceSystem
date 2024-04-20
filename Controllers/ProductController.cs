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

            var items = _context.Items.Include(e => e.MiniSubCategory).Select(i => new
            {
                MiniSubCategoryTLAR = i.MiniSubCategory.MiniSubCategoryTLAR,
                MiniSubCategoryTLEN = i.MiniSubCategory.MiniSubCategoryTLEN,
                MiniSubCategory = i.MiniSubCategory,
                MiniSubCategoryId = i.MiniSubCategoryId,
                ItemId = i.ItemId,
                ItemImage = i.ItemImage,
                ItemTitleAr = i.ItemTitleAr,
                ItemTitleEn = i.ItemTitleEn,
                OldPrice = i.OldPrice,
                OurSellingPrice = i.OurSellingPrice,
                ItemPrice = i.ItemPrice,
                isFav = _context.ProductFavourites.Any(o => o.ItemId == i.ItemId && o.CustomerId == customerId),
                count = 0

            });


            if (catagoriyId != 0)
            {
                items = items.Where(e => e.MiniSubCategoryId == catagoriyId.Value);
            }

            if (query != null)
            {
                // items = items.Where(e => e.MiniSubCategory.MiniSubCategoryTLEN.Contains(query) || e.MiniSubCategory.MiniSubCategoryTLAR.Contains(query) || e.ItemTitleAr.Contains(query) || e.ItemTitleEn.Contains(query));
                items = items.Where(e => e.ItemTitleEn.ToUpper().Contains(query.ToUpper()) || e.ItemTitleAr.ToUpper().Contains(query.ToUpper()));
            }
            int itemsCount = items.Count();
            items = items.Select(i => new
            {
                MiniSubCategoryTLAR = i.MiniSubCategory.MiniSubCategoryTLAR,
                MiniSubCategoryTLEN = i.MiniSubCategory.MiniSubCategoryTLEN,
                MiniSubCategory = i.MiniSubCategory,
                MiniSubCategoryId = i.MiniSubCategoryId,
                ItemId = i.ItemId,
                ItemImage = i.ItemImage,
                ItemTitleAr = i.ItemTitleAr,
                ItemTitleEn = i.ItemTitleEn,
                OldPrice = i.OldPrice,
                OurSellingPrice = i.OurSellingPrice,
                ItemPrice = i.ItemPrice,
                isFav = _context.ProductFavourites.Any(o => o.ItemId == i.ItemId && o.CustomerId == customerId),
                count = itemsCount

            });


            ////var items = _context.Items.Include(e => e.MiniSubCategory).Select(i => new DataGridProductReturn
            ////{
            ////    MiniSubCategoryTLAR=i.MiniSubCategory.MiniSubCategoryTLAR,
            ////    MiniSubCategoryTLEN=i.MiniSubCategory.MiniSubCategoryTLEN,
            ////    MiniSubCategory=i.MiniSubCategory,
            ////    MiniSubCategoryId=i.MiniSubCategoryId,
            ////    ItemId=i.ItemId,
            ////    ItemImage=i.ItemImage,
            ////    ItemTitleAr=i.ItemTitleAr,
            ////    ItemTitleEn=i.ItemTitleEn,
            ////    OldPrice=i.OldPrice,
            ////    OurSellingPrice = i.OurSellingPrice,
            ////    ItemPrice=i.ItemPrice,
            ////    isFav = _context.ProductFavourites.Any(o => o.ItemId == i.ItemId && o.CustomerId == customerId),
            ////    count = 0

            ////}) ;


            ////if (catagoriyId != 0)
            ////{
            ////    items = items.Where(e => e.MiniSubCategoryId == catagoriyId.Value);
            ////}

            ////if (query != null)
            ////{
            ////   // items = items.Where(e => e.MiniSubCategory.MiniSubCategoryTLEN.Contains(query) || e.MiniSubCategory.MiniSubCategoryTLAR.Contains(query) || e.ItemTitleAr.Contains(query) || e.ItemTitleEn.Contains(query));
            ////    items = items.Where(e => e.ItemTitleEn.ToUpper().Contains(query.ToUpper())||e.ItemTitleAr.ToUpper().Contains(query.ToUpper()));
            ////}
            ////int itemsCount = items.Count();
            ////items =items.Select(i => new DataGridProductReturn
            ////{
            ////    MiniSubCategoryTLAR = i.MiniSubCategory.MiniSubCategoryTLAR,
            ////    MiniSubCategoryTLEN = i.MiniSubCategory.MiniSubCategoryTLEN,
            ////    MiniSubCategory = i.MiniSubCategory,
            ////    MiniSubCategoryId = i.MiniSubCategoryId,
            ////    ItemId = i.ItemId,
            ////    ItemImage = i.ItemImage,
            ////    ItemTitleAr = i.ItemTitleAr,
            ////    ItemTitleEn = i.ItemTitleEn,
            ////    OldPrice = i.OldPrice,
            ////    OurSellingPrice = i.OurSellingPrice,
            ////    ItemPrice = i.ItemPrice,
            ////    isFav = _context.ProductFavourites.Any(o => o.ItemId == i.ItemId && o.CustomerId == customerId),
            ////    count = itemsCount

            ////});






            return Json(await DataSourceLoader.LoadAsync(items,loadOptions));
        }


    }
}