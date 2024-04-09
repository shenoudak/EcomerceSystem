using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Jovera.Models
    
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreImage { get; set; }
        public string TradeName { get; set; }
        public string ResponsibleForSupply { get; set; }
        public string LicensePhoto { get; set; }
        public string IdPhoto { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string TaxingNumber { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public double ShareRatio { get; set; }
        public double Depit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public string IPan { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public bool AddingTax { get; set; }
        public string CatagoriesTypes { get; set; }
        public string OtherCatagories { get; set; }
        //public string OtherCatagories { get; set; }
        public bool IsActive { get; set; }
        public int StoreProfileStatusId { get; set; }
        public string Email { get; set; }
        public string RejectProfileReason { get; set; }
        [JsonIgnore]
        public virtual ICollection<StoreProfileImage> StoreProfileImages { get; set; }
        [JsonIgnore]
        public virtual StoreProfileStatus StoreProfileStatus { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }

    }
}
