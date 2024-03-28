using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jovera.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemTitleAr { get; set; }
        public string ItemTitleEn { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ItemImage { get; set; }
        public string BarCode { get; set; }
        public string ItemDescriptionAr { get; set; }
        public string ItemDescriptionEn { get; set; }
        public double ItemPrice { get; set; }
        public bool IsActive { get; set; }
        public bool HasSubProduct { get; set; }
        public int  OrderIndex { get; set; }
        public string  VideoUrl { get; set; }
        public int  Quantity { get; set; }
        public bool OutOfStock { get; set; }
        public int MiniSubCategoryId { get; set; }
        public int? StoreId { get; set; }
        [JsonIgnore]
        public virtual MiniSubCategory MiniSubCategory { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        [JsonIgnore]
        public virtual ICollection<ItemImage> ItemImages { get; set; }
        public virtual ICollection<SubProduct> SubProducts { get; set; }

        
    }
}
