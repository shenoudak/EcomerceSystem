﻿// <auto-generated />
using System;
using Jovera.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jovera.Migrations.CRMDB
{
    [DbContext(typeof(CRMDBContext))]
    [Migration("20240330085410_addUpdate2StoreProfileStatusMig")]
    partial class addUpdate2StoreProfileStatusMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Jovera.Models.AffiliateRatio", b =>
                {
                    b.Property<int>("AffiliateRatioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AffiliateRatioId"), 1L, 1);

                    b.Property<int>("ratioInPrecentage")
                        .HasColumnType("int");

                    b.HasKey("AffiliateRatioId");

                    b.ToTable("AffiliateRatios");
                });

            modelBuilder.Entity("Jovera.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryTLAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryTLEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Jovera.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"), 1L, 1);

                    b.Property<string>("ColorTLAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColorTLEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Jovera.Models.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SendingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Jovera.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<double>("AffiliateBalance")
                        .HasColumnType("float");

                    b.Property<int?>("AffiliateId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Jovera.Models.FAQ", b =>
                {
                    b.Property<int>("FAQId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FAQId"), 1L, 1);

                    b.Property<string>("AnswerAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FAQId");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("Jovera.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"), 1L, 1);

                    b.Property<string>("BarCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasSubProduct")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDescriptionAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemDescriptionEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<string>("ItemTitleAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemTitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MiniSubCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<bool>("OutOfStock")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("MiniSubCategoryId");

                    b.HasIndex("StoreId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Jovera.Models.ItemImage", b =>
                {
                    b.Property<int>("ItemImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemImageId"), 1L, 1);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("ItemImageId");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemImages");
                });

            modelBuilder.Entity("Jovera.Models.MiniSubCategory", b =>
                {
                    b.Property<int>("MiniSubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MiniSubCategoryId"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MiniSubCategoryPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniSubCategoryTLAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniSubCategoryTLEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("MiniSubCategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("MiniSubCategories");
                });

            modelBuilder.Entity("Jovera.Models.PageContent", b =>
                {
                    b.Property<int>("PageContentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PageContentId"), 1L, 1);

                    b.Property<string>("ContentAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitleEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PageContentId");

                    b.ToTable("PageContents");

                    b.HasData(
                        new
                        {
                            PageContentId = 1,
                            ContentAr = "من نحن",
                            ContentEn = "About Page",
                            PageTitleAr = "من نحن",
                            PageTitleEn = "About"
                        },
                        new
                        {
                            PageContentId = 2,
                            ContentAr = "الشروط والاحكام",
                            ContentEn = "Condition and Terms Page",
                            PageTitleAr = "الشروط والاحكام",
                            PageTitleEn = "Condition and Terms"
                        },
                        new
                        {
                            PageContentId = 3,
                            ContentAr = "سياسة الخصوصية",
                            ContentEn = "Privacy Policy Page",
                            PageTitleAr = "سياسة الخصوصية",
                            PageTitleEn = "Privacy Policy"
                        });
                });

            modelBuilder.Entity("Jovera.Models.PaymentMehod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"), 1L, 1);

                    b.Property<string>("PaymentMethodNameAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethodNameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethodPic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMehods");
                });

            modelBuilder.Entity("Jovera.Models.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"), 1L, 1);

                    b.Property<string>("SizeTLAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeTLEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Jovera.Models.SoicialMidiaLink", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Instgramlink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedInlink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TwitterLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsApplink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YoutubeLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("facebooklink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("SoicialMidiaLinks");
                });

            modelBuilder.Entity("Jovera.Models.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"), 1L, 1);

                    b.Property<bool>("AddingTax")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<string>("CatagoriesTypes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Credit")
                        .HasColumnType("float");

                    b.Property<double>("Depit")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdPhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicensePhoto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RejectProfileReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsibleForSupply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ShareRatio")
                        .HasColumnType("float");

                    b.Property<string>("StoreImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreProfileStatusId")
                        .HasColumnType("int");

                    b.Property<string>("TradeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.HasIndex("StoreProfileStatusId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Jovera.Models.StoreProfileImage", b =>
                {
                    b.Property<int>("StoreProfileImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreProfileImageId"), 1L, 1);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("StoreProfileImageId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreProfileImages");
                });

            modelBuilder.Entity("Jovera.Models.StoreProfileStatus", b =>
                {
                    b.Property<int>("StoreProfileStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreProfileStatusId"), 1L, 1);

                    b.Property<string>("StatusArabicTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusEnglishTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreProfileStatusId");

                    b.ToTable("StoreProfileStatuses");
                });

            modelBuilder.Entity("Jovera.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryTLAR")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubCategoryTLEN")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Jovera.Models.SubProduct", b =>
                {
                    b.Property<int>("SubProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubProductId"), 1L, 1);

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("ItemQRCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("SizeId")
                        .HasColumnType("int");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("SubProductId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ItemId");

                    b.HasIndex("SizeId");

                    b.HasIndex("StoreId");

                    b.ToTable("SubProducts");
                });

            modelBuilder.Entity("Jovera.Models.Item", b =>
                {
                    b.HasOne("Jovera.Models.MiniSubCategory", "MiniSubCategory")
                        .WithMany("Items")
                        .HasForeignKey("MiniSubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jovera.Models.Store", "Store")
                        .WithMany("Items")
                        .HasForeignKey("StoreId");

                    b.Navigation("MiniSubCategory");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Jovera.Models.ItemImage", b =>
                {
                    b.HasOne("Jovera.Models.Item", "Item")
                        .WithMany("ItemImages")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Jovera.Models.MiniSubCategory", b =>
                {
                    b.HasOne("Jovera.Models.SubCategory", "SubCategory")
                        .WithMany("MiniSubCategories")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Jovera.Models.Store", b =>
                {
                    b.HasOne("Jovera.Models.StoreProfileStatus", "StoreProfileStatus")
                        .WithMany()
                        .HasForeignKey("StoreProfileStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StoreProfileStatus");
                });

            modelBuilder.Entity("Jovera.Models.StoreProfileImage", b =>
                {
                    b.HasOne("Jovera.Models.Store", "Store")
                        .WithMany("StoreProfileImages")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Jovera.Models.SubCategory", b =>
                {
                    b.HasOne("Jovera.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Jovera.Models.SubProduct", b =>
                {
                    b.HasOne("Jovera.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.HasOne("Jovera.Models.Item", "Item")
                        .WithMany("SubProducts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jovera.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId");

                    b.HasOne("Jovera.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId");

                    b.Navigation("Color");

                    b.Navigation("Item");

                    b.Navigation("Size");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Jovera.Models.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Jovera.Models.Item", b =>
                {
                    b.Navigation("ItemImages");

                    b.Navigation("SubProducts");
                });

            modelBuilder.Entity("Jovera.Models.MiniSubCategory", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Jovera.Models.Store", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("StoreProfileImages");
                });

            modelBuilder.Entity("Jovera.Models.SubCategory", b =>
                {
                    b.Navigation("MiniSubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
