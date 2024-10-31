﻿// <auto-generated />
using System;
using LAB6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LAB6.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LAB6.Models.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountNumber"));

                    b.Property<string>("AccountStatusCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OtherDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountNumber");

                    b.HasIndex("AccountTypeCode");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LAB6.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateProvinceCounty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TownCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipPostcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("LAB6.Models.Bank", b =>
                {
                    b.Property<Guid>("BankId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BankDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BankId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("LAB6.Models.Branch", b =>
                {
                    b.Property<Guid>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<Guid>("BankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BranchDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BranchId");

                    b.HasIndex("AddressId");

                    b.HasIndex("BankId");

                    b.HasIndex("BranchTypeCode");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("LAB6.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("AddressId");

                    b.HasIndex("BranchId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LAB6.Models.RefAccountType", b =>
                {
                    b.Property<string>("AccountTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountTypeCode");

                    b.ToTable("RefAccountTypes");
                });

            modelBuilder.Entity("LAB6.Models.RefBranchType", b =>
                {
                    b.Property<string>("BranchTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BranchTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchTypeCode");

                    b.ToTable("RefBranchTypes");
                });

            modelBuilder.Entity("LAB6.Models.RefTransactionType", b =>
                {
                    b.Property<string>("TransactionTypeCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TransactionTypeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionTypeCode");

                    b.ToTable("RefTransactionTypes");
                });

            modelBuilder.Entity("LAB6.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("MerchantId")
                        .HasColumnType("int");

                    b.Property<string>("OtherDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionTypeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("TransactionTypeCode");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("LAB6.Models.Account", b =>
                {
                    b.HasOne("LAB6.Models.RefAccountType", "AccountType")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountTypeCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LAB6.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AccountType");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LAB6.Models.Branch", b =>
                {
                    b.HasOne("LAB6.Models.Address", "Address")
                        .WithMany("Branches")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LAB6.Models.Bank", "Bank")
                        .WithMany("Branches")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LAB6.Models.RefBranchType", "BranchType")
                        .WithMany("Branches")
                        .HasForeignKey("BranchTypeCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Bank");

                    b.Navigation("BranchType");
                });

            modelBuilder.Entity("LAB6.Models.Customer", b =>
                {
                    b.HasOne("LAB6.Models.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LAB6.Models.Branch", "Branch")
                        .WithMany("Customers")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("LAB6.Models.Transaction", b =>
                {
                    b.HasOne("LAB6.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LAB6.Models.RefTransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionTypeCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("TransactionType");
                });

            modelBuilder.Entity("LAB6.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LAB6.Models.Address", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("LAB6.Models.Bank", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("LAB6.Models.Branch", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("LAB6.Models.Customer", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("LAB6.Models.RefAccountType", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("LAB6.Models.RefBranchType", b =>
                {
                    b.Navigation("Branches");
                });

            modelBuilder.Entity("LAB6.Models.RefTransactionType", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
