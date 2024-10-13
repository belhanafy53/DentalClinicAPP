using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public partial class InvoiceEF : DbContext
    {
        public InvoiceEF()
            : base("name=InvoiceEF")
        {



        }

        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<ErrorDetail> ErrorDetails { get; set; }
        public virtual DbSet<EtaReponce> EtaReponces { get; set; }
        public virtual DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public virtual DbSet<InvoiceHeadeStr> InvoiceHeadeStrs { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<InvoiceLine_TaxableItem> InvoiceLine_TaxableItem { get; set; }
        public virtual DbSet<InvoiceLined> InvoiceLineds { get; set; }
        public virtual DbSet<InvoiceLinedstr> InvoiceLinedstrs { get; set; }
        public virtual DbSet<Issuer> Issuers { get; set; }
        public virtual DbSet<Issuer_Address> Issuer_Address { get; set; }
        public virtual DbSet<OracleInvoice> OracleInvoices { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Receiver> Receivers { get; set; }
        public virtual DbSet<Receiver_Address> Receiver_Address { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SendStatu> SendStatus { get; set; }
        public virtual DbSet<Signature> Signatures { get; set; }
        public virtual DbSet<TaxableItem> TaxableItems { get; set; }
        public virtual DbSet<TaxTotal> TaxTotals { get; set; }
        public virtual DbSet<UnitValue> UnitValues { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>()
                .Property(e => e.grossWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Delivery>()
                .Property(e => e.netWeight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Discount>()
                .Property(e => e.rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Discount>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<document>()
                .Property(e => e.totalSalesAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.totalDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.netAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.extraDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.totalItemsDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .Property(e => e.totalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<document>()
                .HasOptional(e => e.Issuer)
                .WithRequired(e => e.document);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.totalDiscountAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.totalSalesAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.netAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.taxTotals__amount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.totalAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.extraDiscountAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeader>()
                .Property(e => e.totalItemsDiscountAmount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceHeadeStr>()
                .HasMany(e => e.InvoiceLinedstrs)
                .WithOptional(e => e.InvoiceHeadeStr)
                .HasForeignKey(e => e.InvoiceHeaderID);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.salesTotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.valueDifference)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.totalTaxableFees)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.netTotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLine>()
                .Property(e => e.itemsDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.quantity)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.salesTotal)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.total)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.valueDifference)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.totalTaxableFees)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.netTotal)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.itemsDiscount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.unitValue_amountEGP)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.discount_rate)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.discount_amount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.taxableItems_amount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.taxableItems_rate)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.unitValue_amountSold)
                .HasPrecision(18, 5);

            modelBuilder.Entity<InvoiceLined>()
                .Property(e => e.unitValue_currencyExchangeRate)
                .HasPrecision(18, 5);

            modelBuilder.Entity<Issuer>()
                .HasMany(e => e.Issuer_Address)
                .WithOptional(e => e.Issuer)
                .HasForeignKey(e => e.IssuerID);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__salesTotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__valueDifference)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__totalTaxableFees)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__netTotal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__itemsDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__unitValue__amountEGP)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.totalDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.totalSalesAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.netAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.taxTotals__amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.totalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.extraDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.totalItemsDiscountAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__discount__rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__discount__amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__taxableItems__amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OracleInvoice>()
                .Property(e => e.invoiceLines__taxableItems__rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Receiver>()
                .HasMany(e => e.Receiver_Address)
                .WithOptional(e => e.Receiver)
                .HasForeignKey(e => e.ReceiverID);

            modelBuilder.Entity<SendStatu>()
                .HasMany(e => e.InvoiceHeaders)
                .WithOptional(e => e.SendStatu)
                .HasForeignKey(e => e.SendStatusId);

            modelBuilder.Entity<SendStatu>()
                .HasMany(e => e.InvoiceHeadeStrs)
                .WithOptional(e => e.SendStatu)
                .HasForeignKey(e => e.SendStatusId);

            modelBuilder.Entity<SendStatu>()
                .HasMany(e => e.OracleInvoices)
                .WithOptional(e => e.SendStatu)
                .HasForeignKey(e => e.SendStatusId);

            modelBuilder.Entity<TaxableItem>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TaxableItem>()
                .Property(e => e.rate)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TaxableItem>()
                .HasMany(e => e.InvoiceLine_TaxableItem)
                .WithOptional(e => e.TaxableItem)
                .HasForeignKey(e => e.TaxableId);

            modelBuilder.Entity<TaxTotal>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<UnitValue>()
                .Property(e => e.amountEGP)
                .HasPrecision(18, 0);
        }
    }
}
