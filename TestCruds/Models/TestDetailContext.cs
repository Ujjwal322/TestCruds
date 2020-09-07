using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestCruds.Models
{
    public partial class TestDetailContext : DbContext
    {
        public TestDetailContext()
        {
        }

        public TestDetailContext(DbContextOptions<TestDetailContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerTbl> CustomerTbl { get; set; }
        public virtual DbSet<InvoiceTbl> InvoiceTbl { get; set; }
        public virtual DbSet<PaymentTbl> PaymentTbl { get; set; }
        //public  DbSet<Invoice> Invoices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=10.61.18.12;Initial Catalog=TestDetail;Persist Security Info=True;User ID=msdba;Password=dba@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<CustomerTbl>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvoiceTbl>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.InvoiceAmount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDueDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InvoiceTbl)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_InvoiceTbl_InvoiceTbl");
            });

            modelBuilder.Entity<PaymentTbl>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.PaymentAmount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentDate).HasColumnType("date");

                entity.Property(e => e.PaymentNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.PaymentTbl)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_PaymentTbl_InvoiceTbl");
            });
        }
    }
}
