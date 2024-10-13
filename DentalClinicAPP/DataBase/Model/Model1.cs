namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Tbl_Accounting_Guid> Tbl_Accounting_Guid { get; set; }
        public virtual DbSet<Tbl_AccountsKind> Tbl_AccountsKind { get; set; }
        public virtual DbSet<Tbl_Activities> Tbl_Activities { get; set; }
        public virtual DbSet<Tbl_Cities> Tbl_Cities { get; set; }
        public virtual DbSet<Tbl_Clinics> Tbl_Clinics { get; set; }
        public virtual DbSet<Tbl_Companies> Tbl_Companies { get; set; }
        public virtual DbSet<Tbl_CompanyType> Tbl_CompanyType { get; set; }
        public virtual DbSet<Tbl_Country> Tbl_Country { get; set; }
        public virtual DbSet<Tbl_Currency> Tbl_Currency { get; set; }
        public virtual DbSet<Tbl_Customer> Tbl_Customer { get; set; }
        public virtual DbSet<Tbl_Device> Tbl_Device { get; set; }
        public virtual DbSet<Tbl_DocumentType> Tbl_DocumentType { get; set; }
        public virtual DbSet<Tbl_Governerate> Tbl_Governerate { get; set; }
        public virtual DbSet<Tbl_Items> Tbl_Items { get; set; }
        public virtual DbSet<Tbl_ItemsOfTax> Tbl_ItemsOfTax { get; set; }
        public virtual DbSet<Tbl_Management> Tbl_Management { get; set; }
        public virtual DbSet<Tbl_ManagementCategory> Tbl_ManagementCategory { get; set; }
        public virtual DbSet<Tbl_OurCompActivity> Tbl_OurCompActivity { get; set; }
        public virtual DbSet<Tbl_OurCompany> Tbl_OurCompany { get; set; }
        public virtual DbSet<Tbl_OurCompanyCurrency> Tbl_OurCompanyCurrency { get; set; }
        public virtual DbSet<Tbl_OurCompanyUnite> Tbl_OurCompanyUnite { get; set; }
        public virtual DbSet<Tbl_PaymentMethods> Tbl_PaymentMethods { get; set; }
        public virtual DbSet<Tbl_ReasonType> Tbl_ReasonType { get; set; }
        public virtual DbSet<Tbl_Sectors> Tbl_Sectors { get; set; }
        public virtual DbSet<Tbl_TaxOurCompany> Tbl_TaxOurCompany { get; set; }
        public virtual DbSet<Tbl_TaxSubType> Tbl_TaxSubType { get; set; }
        public virtual DbSet<Tbl_TaxType> Tbl_TaxType { get; set; }
        public virtual DbSet<Tbl_Token> Tbl_Token { get; set; }
        public virtual DbSet<Tbl_UniteType> Tbl_UniteType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_AccountsKind>()
                .HasMany(e => e.Tbl_Accounting_Guid)
                .WithOptional(e => e.Tbl_AccountsKind)
                .HasForeignKey(e => e.AccountsKind_ID);

            modelBuilder.Entity<Tbl_AccountsKind>()
                .HasMany(e => e.Tbl_AccountsKind1)
                .WithOptional(e => e.Tbl_AccountsKind2)
                .HasForeignKey(e => e.Parent_id);

            modelBuilder.Entity<Tbl_Activities>()
                .HasMany(e => e.Tbl_OurCompActivity)
                .WithOptional(e => e.Tbl_Activities)
                .HasForeignKey(e => e.ActivityCode);

            modelBuilder.Entity<Tbl_CompanyType>()
                .Property(e => e.Name_E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_CompanyType>()
                .Property(e => e.Name_Ar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_CompanyType>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Customer>()
                .Property(e => e.CompanyType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_DocumentType>()
                .Property(e => e.Name_E)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_DocumentType>()
                .Property(e => e.Name_Ar)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_DocumentType>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Governerate>()
                .HasMany(e => e.Tbl_Cities)
                .WithRequired(e => e.Tbl_Governerate)
                .HasForeignKey(e => e.GovernorateID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Items>()
                .HasMany(e => e.Tbl_Items1)
                .WithOptional(e => e.Tbl_Items2)
                .HasForeignKey(e => e.Parent_ID);

            modelBuilder.Entity<Tbl_ManagementCategory>()
                .HasMany(e => e.Tbl_Management)
                .WithOptional(e => e.Tbl_ManagementCategory)
                .HasForeignKey(e => e.ManagementCategory_ID);

            modelBuilder.Entity<Tbl_OurCompany>()
                .Property(e => e.BuildingNumber)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_OurCompany>()
                .Property(e => e.Floor)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_OurCompany>()
                .Property(e => e.room)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_OurCompany>()
                .Property(e => e.CompanyType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_OurCompany>()
                .HasMany(e => e.Tbl_OurCompActivity)
                .WithOptional(e => e.Tbl_OurCompany)
                .HasForeignKey(e => e.OurCompanyRef);

            modelBuilder.Entity<Tbl_TaxOurCompany>()
                .Property(e => e.TaxRate)
                .HasPrecision(5, 0);

            modelBuilder.Entity<Tbl_TaxType>()
                .HasMany(e => e.Tbl_TaxSubType)
                .WithOptional(e => e.Tbl_TaxType)
                .HasForeignKey(e => e.TaxTypeRefrence);
        }
    }
}
