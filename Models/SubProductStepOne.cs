using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class SubProductStepOne
    {
        [Key]
        public int SubProductStepOneId { get; set; }
        public int StepOneId { get; set; }
        public int ItemId { get; set; }
        public string Icon { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual StepOne StepOne { get; set; }
        [JsonIgnore]
        public virtual Item Item { get; set; }
        [JsonIgnore]
        public virtual ICollection<MiniSubProduct> MiniSubProducts { get; set; }
    }
}
