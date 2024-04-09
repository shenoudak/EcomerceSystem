using System.ComponentModel.DataAnnotations;
namespace Jovera.Models
{
    public class ItemStatus
    {
        [Key]
        public int ItemStatusId { get; set; }
        public string StatusArabicTitle { get; set; }
        public string StatusEnglishTitle { get; set; }

    }
}
