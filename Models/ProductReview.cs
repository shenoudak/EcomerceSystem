using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Review { get; set; }
		public int ItemId { get; set; }
		[JsonIgnore]
		public virtual Item Item { get; set; }
		[JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
