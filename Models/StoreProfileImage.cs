using System.Text.Json.Serialization;

namespace Jovera.Models
{
    public class StoreProfileImage
    {
        public int StoreProfileImageId { get; set; }
        public int StoreId { get; set; }
        public string ImageName { get; set; }
        [JsonIgnore]
        public virtual Store Store { get; set; }
    }
}
