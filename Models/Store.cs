using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
    
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }

    }
}
