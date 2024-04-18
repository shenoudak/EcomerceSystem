using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class SubProduct
    {
        public int SubProductId { get; set; }
        public int ItemId { get; set; }
       
        public int? StepOneId { get; set; }
        public int? StepTwoId { get; set; }
        public int? Quantity { get; set; }
        public string ItemQRCode { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual StepOne StepOne { get; set; }
        [JsonIgnore]
        public virtual StepTwo StepTwo { get; set; }
        public int? StoreId { get; set; }
        
        [JsonIgnore]
        public virtual Store Store { get; set; }
    }
}
