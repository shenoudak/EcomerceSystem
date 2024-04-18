using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class StepTwo
    {
        [Key]
        public int StepTwoId { get; set; }
        [Required]
        public string StepTwoTLAR { get; set; }
        [Required]
        public string StepTwoTLEN { get; set; }
        public bool IsDeleted { get; set; }
        public int StepOneId { get; set; }
        [JsonIgnore]
        public virtual StepOne StepOne { get; set; }
    }
}
