using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class StepOne
    {
        [Key]
        public int StepOneId { get; set; }
        [Required]
        public string StepOneTLAR { get; set; }
        [Required]
        public string StepOneTLEN { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<StepTwo> StepTwos { get; set; }
    }
}
