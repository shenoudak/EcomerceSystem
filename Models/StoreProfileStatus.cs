using NuGet.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class StoreProfileStatus
    {
        [Key]
        public int StoreProfileStatusId { get; set; }
        public string StatusArabicTitle { get; set; }
        public string StatusEnglishTitle { get; set; }
       
       
    }
}
