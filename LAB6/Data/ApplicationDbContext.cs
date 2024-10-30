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
                .HasForeignKey(b => b.BankId);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Address)
                .WithMany(a => a.Branches)
                .HasForeignKey(b => b.AddressId);

            modelBuilder.Entity<Branch>()
                .HasOne(b => b.BranchType)
                .WithMany(rt => rt.Branches)
                .HasForeignKey(b => b.BranchTypeCode);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Branch)
                .WithMany(b => b.Customers)
                .HasForeignKey(c => c.BranchId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AddressId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountType)
                .WithMany(rt => rt.Accounts)
                .HasForeignKey(a => a.AccountTypeCode);

            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

            modelBuilder.Entity<Models.Transaction>()
                .HasOne(t => t.TransactionType)
                .WithMany(tt => tt.Transactions)
                .HasForeignKey(t => t.TransactionTypeCode);

            // Configure field properties
            modelBuilder.Entity<Bank>().Property(b => b.BankId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Branch>().Property(b => b.BranchId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().Property(c => c.CustomerId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Models.Transaction>().Property(t => t.TransactionId).ValueGeneratedOnAdd();

         
        }

        public void Seed()
        {
            // Перевіряємо, чи вже є дані, щоб уникнути дублювання
            if (!Banks.Any())
            {
                var banks = new List<Bank>
            {
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Bank A" },
                new Bank { BankId = Guid.NewGuid(), BankDetails = "Bank B" }
            };
                Banks.AddRange(banks);
                SaveChanges();

                var branches = new List<Branch>
            {
                new Branch { BranchId = Guid.NewGuid(), BankId = banks[0].BankId, BranchTypeCode = "URB", BranchDetails = "Main Branch" },
                new Branch { BranchId = Guid.NewGuid(), BankId = banks[1].BankId, BranchTypeCode = "RUR", BranchDetails = "Rural Branch" }
            };
                Branches.AddRange(branches);
                SaveChanges();

                var customers = new List<Customer>
            {
                new Customer { CustomerId = Guid.NewGuid(), BranchId = branches[0].BranchId, PersonalDetails = "John Doe", ContactDetails = "john@example.com" },
                new Customer { CustomerId = Guid.NewGuid(), BranchId = branches[1].BranchId, PersonalDetails = "Jane Smith", ContactDetails = "jane@example.com" }
            };
                Customers.AddRange(customers);
                SaveChanges();

                var accounts = new List<Account>
            {
                new Account { AccountNumber = 1, CustomerId = customers[0].CustomerId, AccountTypeCode = "CHK", CurrentBalance = 1000.00M, OtherDetails = "Primary Checking Account" },
                new Account { AccountNumber = 2, CustomerId = customers[1].CustomerId, AccountTypeCode = "SAV", CurrentBalance = 5000.00M, OtherDetails = "Primary Savings Account" }
            };
                Accounts.AddRange(accounts);
                SaveChanges();

                var transactions = new List<Models.Transaction>
            {
                new Models.Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[0].AccountNumber, TransactionTypeCode = "DEP", TransactionDateTime = DateTime.Now, TransactionAmount = 100.00M, OtherDetails = "Initial Deposit" },
                new Models.Transaction { TransactionId = Guid.NewGuid(), AccountNumber = accounts[1].AccountNumber, TransactionTypeCode = "WDL", TransactionDateTime = DateTime.Now, TransactionAmount = 50.00M, OtherDetails = "ATM Withdrawal" }
            };
                Transactions.AddRange(transactions);
                SaveChanges();
            }
        }
    }
}

