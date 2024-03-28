using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }
        [Required]
        public string SizeTLAR { get; set; }
        [Required]
        public string SizeTLEN { get; set; }
    }
}
