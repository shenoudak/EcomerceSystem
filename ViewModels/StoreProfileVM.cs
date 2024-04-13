using System.ComponentModel.DataAnnotations;
namespace Jovera.ViewModels
{
    public class StoreProfileVM
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreImage { get; set; }
        [Required(ErrorMessage = "Enter Trade Name")]
        public string TradeName { get; set; }
        [Required(ErrorMessage = "Responsible Name For Supply")]
        public string ResponsibleForSupply { get; set; }
     
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        [Required(ErrorMessage = "Enter Share Ratio")]
        public double ShareRatio { get; set; }
        [Required(ErrorMessage = "Enter IPan")]
        public string IPan { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string OtherCatagory { get; set; }
        public bool AddingTax { get; set; }
        public bool HasOtherCatgory { get; set; }
        public string LicensePhoto { get; set; }
        public string IdPhoto { get; set; }
        public string TaxingNumber { get; set; }
        [Required(ErrorMessage = "Enter Catagories Types Of Your Products")]
        public string CatagoriesTypes { get; set; }
       
    }
}
