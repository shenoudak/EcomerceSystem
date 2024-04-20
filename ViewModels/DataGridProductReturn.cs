using Jovera.Models;

namespace Jovera.ViewModels
{
    public class DataGridProductReturn
    {
        public MiniSubCategory MiniSubCategory { get; set; }
        public int ItemId { get; set; }
        public int MiniSubCategoryId { get; set; }
        public string MiniSubCategoryTLEN { get; set; }
        public string MiniSubCategoryTLAR { get; set; }
        public string ItemTitleAr { get; set; }
        public string ItemTitleEn { get; set; }
        public string ItemImage { get; set; }
        public double ItemPrice { get; set; }
        public double OurSellingPrice { get; set; }
        public double OldPrice { get; set; }
        public bool isFav { get; set; }
        public int count { get; set; }
        
    }
}
