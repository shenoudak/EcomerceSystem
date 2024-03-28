using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Jovera.Models
{
    public class MiniSubCategory
    {

        [Key]
        public int MiniSubCategoryId { get; set; }
        public string MiniSubCategoryTLAR { get; set; }
        public string MiniSubCategoryTLEN { get; set; }
        public string MiniSubCategoryPic { get; set; }
        public bool IsActive { get; set; }
        public int OrderIndex { get; set; }
        public int SubCategoryId { get; set; }
        [JsonIgnore]
        public virtual SubCategory SubCategory { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }
    }
}
