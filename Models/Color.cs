using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [Required]
        public string ColorTLAR { get; set; }
        [Required]
        public string ColorTLEN { get; set; }
    }
}
