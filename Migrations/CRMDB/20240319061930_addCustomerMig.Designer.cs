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
    [Migration("20240319061930_addCustomerMig")]
    partial class addCustomerMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
#pragma warning restore 612, 618
        }
    }
}
