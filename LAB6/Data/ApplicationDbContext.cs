using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Transactions;
using LAB6.Models;

namespace LAB6.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Models.Transaction> Transactions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefAccountType> RefAccountTypes { get; set; }
        public DbSet<RefBranchType> RefBranchTypes { get; set; }
        public DbSet<RefTransactionType> RefTransactionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Bank>().HasKey(b => b.BankId);
            modelBuilder.Entity<Branch>().HasKey(b => b.BranchId);
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<Account>().HasKey(a => a.AccountNumber);
            modelBuilder.Entity<Models.Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
            modelBuilder.Entity<RefAccountType>().HasKey(r => r.AccountTypeCode);
            modelBuilder.Entity<RefBranchType>().HasKey(r => r.BranchTypeCode);
            modelBuilder.Entity<RefTransactionType>().HasKey(r => r.TransactionTypeCode);

            // Configure relationships

            modelBuilder.Entity<Branch>()
        .HasOne(b => b.Bank)
        .WithMany(bk => bk.Branches)
        .HasForeignKey(b => b.BankId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Address)
                .WithMany(a => a.Branches)
                .HasForeignKey(b => b.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.BranchType)
                .WithMany(rt => rt.Branches)
                .HasForeignKey(b => b.BranchTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Branch)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountType)
                .WithMany(rt => rt.Accounts)
                .HasForeignKey(a => a.AccountTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.TransactionType)
                .WithMany(tt => tt.Transactions)
                .HasForeignKey(t => t.TransactionTypeCode)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure field properties
            modelBuilder.Entity<Bank>().Property(b => b.BankId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Branch>().Property(b => b.BranchId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(c => c.CustomerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Transaction>().Property(t => t.TransactionId).ValueGeneratedOnAdd();

         
        }

        public void Seed()
        {
            if (Addresses.Any()) return;

            // 1. Seed Reference Tables with fixed-length codes
            var accountTypes = new List<RefAccountType>
            {
                new RefAccountType
                {
                    AccountTypeCode = "SAV",            // Using CHAR(15) format
                    AccountTypeDescription = "Savings Account"
                },
                new RefAccountType
                {
                    AccountTypeCode = "CHK",
                    AccountTypeDescription = "Checking Account"
                },
                new RefAccountType
                {
                    AccountTypeCode = "BUS",
                    AccountTypeDescription = "Business Account"
                }
            };
            RefAccountTypes.AddRange(accountTypes);
            SaveChanges();

            var branchTypes = new List<RefBranchType>
            {
                new RefBranchType
                {
                    BranchTypeCode = "MAIN",           // Using CHAR(15) format
                    BranchTypeDescription = "Main Branch Office"
                },
                new RefBranchType
                {
                    BranchTypeCode = "SAT",
                    BranchTypeDescription = "Satellite Branch"
                }
            };
            RefBranchTypes.AddRange(branchTypes);
            SaveChanges();

            var transactionTypes = new List<RefTransactionType>
            {
                new RefTransactionType
                {
                    TransactionTypeCode = "DEP",        // Using CHAR(15) format
                    TransactionTypeDescription = "Deposit"
                },
                new RefTransactionType
                {
                    TransactionTypeCode = "WD",
                    TransactionTypeDescription = "Withdrawal"
                },
                new RefTransactionType
                {
                    TransactionTypeCode = "TRF",
                    TransactionTypeDescription = "Transfer"
                }
            };
            RefTransactionTypes.AddRange(transactionTypes);
            SaveChanges();

            // 2. Let SQL Server handle the AddressId identity
            var addresses = new List<Address>
            {
                new Address
                {
                    Line1 = "123 Main Street",
                    Line2 = "Suite 100",
                    TownCity = "New York",
                    ZipPostcode = "10001",
                    StateProvinceCounty = "NY",
                    Country = "USA",
                    OtherDetails = "Main Office Location"
                },
                new Address
                {
                    Line1 = "456 Market Street",
                    Line2 = "Suite 150",
                    TownCity = "Los Angeles",
                    ZipPostcode = "90012",
                    StateProvinceCounty = "CA",
                    Country = "USA",
                    OtherDetails = "West Coast Branch"
                }
            };
            Addresses.AddRange(addresses);
            SaveChanges();

            // 3. Seed Banks
            var banks = new List<Bank>
            {
                new Bank
                {
                    BankId = Guid.NewGuid(),
                    BankDetails = "Global Bank Corporation"
                },
                new Bank
                {
                    BankId = Guid.NewGuid(),
                    BankDetails = "City Financial Services"
                }
            };
            Banks.AddRange(banks);
            SaveChanges();

            // 4. Seed Branches
            var branches = new List<Branch>
            {
                new Branch
                {
                    BranchId = Guid.NewGuid(),
                    AddressId = addresses[0].AddressId,  // Now using generated AddressId
                    BankId = banks[0].BankId,
                    BranchTypeCode = "MAIN",
                    BranchDetails = "Main Headquarters Branch"
                },
                new Branch
                {
                    BranchId = Guid.NewGuid(),
                    AddressId = addresses[1].AddressId,  // Now using generated AddressId
                    BankId = banks[0].BankId,
                    BranchTypeCode = "SAT",
                    BranchDetails = "West LA Branch Office"
                }
            };
            Branches.AddRange(branches);
            SaveChanges();

            // 5. Seed Customers
            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    AddressId = addresses[0].AddressId,
                    BranchId = branches[0].BranchId,
                    PersonalDetails = "John Smith",
                    ContactDetails = "Tel: 555-0123, Email: john.smith@email.com"
                },
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    AddressId = addresses[1].AddressId,
                    BranchId = branches[1].BranchId,
                    PersonalDetails = "Jane Doe",
                    ContactDetails = "Tel: 555-0124, Email: jane.doe@email.com"
                }
            };
            Customers.AddRange(customers);
            SaveChanges();

            // 6. Let SQL Server handle the AccountNumber identity
            var accounts = new List<Account>
            {
                new Account
                {
                    AccountStatusCode = "ACT",
                    AccountTypeCode = "SAV",
                    CustomerId = customers[0].CustomerId,
                    CurrentBalance = 5000.00M,
                    OtherDetails = "Primary Savings Account"
                },
                new Account
                {
                    AccountStatusCode = "ACT",
                    AccountTypeCode = "CHK",
                    CustomerId = customers[0].CustomerId,
                    CurrentBalance = 2500.00M,
                    OtherDetails = "Primary Checking Account"
                },
                new Account
                {
                    AccountStatusCode = "ACT",
                    AccountTypeCode = "SAV",
                    CustomerId = customers[1].CustomerId,
                    CurrentBalance = 7500.00M,
                    OtherDetails = "High-Interest Savings Account"
                }
            };
            Accounts.AddRange(accounts);
            SaveChanges();

            // 7. Seed Transactions
            var transactions = new List<Models.Transaction>
            {
                new Models.Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    AccountNumber = 1,  // Reference the first account
                    MerchantId = 1,
                    TransactionTypeCode = "DEP",
                    TransactionDateTime = DateTime.Now.AddDays(-5),
                    TransactionAmount = 1000.00M,
                    OtherDetails = "Initial deposit"
                },
                new Models.Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    AccountNumber = 2,  // Reference the second account
                    MerchantId = 2,
                    TransactionTypeCode = "WD",
                    TransactionDateTime = DateTime.Now.AddDays(-3),
                    TransactionAmount = -500.00M,
                    OtherDetails = "ATM withdrawal"
                },
                new Models.Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    AccountNumber = 3,  // Reference the third account
                    MerchantId = 3,
                    TransactionTypeCode = "TRF",
                    TransactionDateTime = DateTime.Now.AddDays(-1),
                    TransactionAmount = 2500.00M,
                    OtherDetails = "Wire transfer deposit"
                }
            };
            Transactions.AddRange(transactions);
            SaveChanges();
        }
    }
}

