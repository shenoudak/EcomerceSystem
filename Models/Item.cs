using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Jovera.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string ItemTitleAr { get; set; }

        [Required]
        public string ItemTitleEn { get; set; }
        public string ItemImage { get; set; }
        public string BarCode { get; set; }
        public string ItemDescriptionAr { get; set; }
        public string ItemDescriptionEn { get; set; }
        public double ItemPrice { get; set; }
        public bool IsActive { get; set; }
        public int  OrderIndex { get; set; }
        public int  Quantity { get; set; }
        public bool OutOfStock { get; set; }
        public int CategoryId { get; set; }
        public int? StoreId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
        [JsonIgnore]
        public virtual ICollection<ItemImage> ItemImages { get; set; }

        
    }
}
