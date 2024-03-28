using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class SubProduct
    {
        public int SubProductId { get; set; }
        public int ItemId { get; set; }
       
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public int? Quantity { get; set; }
        public string ItemQRCode { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual Color Color { get; set; }
        [JsonIgnore]
        public virtual Size Size { get; set; }
        public int? StoreId { get; set; }
        
        [JsonIgnore]
        public virtual Store Store { get; set; }
    }
}
