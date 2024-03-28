using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Jovera.Models;

#nullable disable

namespace Jovera.Data
{
    public partial class CRMDBContext : DbContext
    {
        public CRMDBContext()
        {

        }

        public CRMDBContext(DbContextOptions<CRMDBContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            OnModelCreatingPartial(modelBuilder);

        }
 
        public virtual DbSet<PaymentMehod> PaymentMehods { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<SoicialMidiaLink> SoicialMidiaLinks { get; set; }
        public virtual DbSet<FAQ> FAQ { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemImage> ItemImages { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<AffiliateRatio> AffiliateRatios { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<MiniSubCategory> MiniSubCategories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<SubProduct> SubProducts { get; set; }
     

   
        

        


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 1, PageTitleAr = "من نحن", PageTitleEn = "About", ContentAr = "من نحن", ContentEn = "About Page" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 2, PageTitleAr = "الشروط والاحكام", PageTitleEn = "Condition and Terms", ContentAr = "الشروط والاحكام", ContentEn = "Condition and Terms Page" });
            modelBuilder.Entity<PageContent>().HasData(new PageContent { PageContentId = 3, PageTitleAr = "سياسة الخصوصية", PageTitleEn = "Privacy Policy", ContentAr = "سياسة الخصوصية", ContentEn = "Privacy Policy Page" });
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

       

    }
}
