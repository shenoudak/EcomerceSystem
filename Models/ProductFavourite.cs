using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class ProductFavourite
    {
        [Key]
        public int ProductFavouriteId { get; set; }
        public int ItemId { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }

    }
}
