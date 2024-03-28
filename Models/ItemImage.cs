using System.Text.Json.Serialization;

namespace Jovera.Models
{
    public class ItemImage
    {
        public int ItemImageId { get; set; }
        public int ItemId { get; set; }
        public string ImageName { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }

    }
}
