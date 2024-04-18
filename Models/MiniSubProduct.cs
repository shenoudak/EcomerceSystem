using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class MiniSubProduct
    {
        [Key]
        public int MiniSubProductId { get; set; }
        public int StepTwoId { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public string ItemQRCode { get; set; }
        public int SubProductStepOneId { get; set; }

        [JsonIgnore]
        public virtual StepTwo StepTwo { get; set; }
        [JsonIgnore]
        public virtual SubProductStepOne SubProductStepOne { get; set; }

    }
}
