namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_OurCompany
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_OurCompany()
        {
            Tbl_OurCompActivity = new HashSet<Tbl_OurCompActivity>();
        }

        public int ID { get; set; }

        [StringLength(150)]
        public string Name_E { get; set; }

        [StringLength(150)]
        public string Name_Ar { get; set; }

        [StringLength(150)]
        public string TaxRegisterNo { get; set; }

        [StringLength(50)]
        public string CountryCode { get; set; }

        public int? GovernorateID { get; set; }

        public int? CityID { get; set; }

        [StringLength(250)]
        public string Street { get; set; }

        [StringLength(5)]
        public string BuildingNumber { get; set; }

        [StringLength(3)]
        public string Floor { get; set; }

        [StringLength(3)]
        public string room { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(10)]
        public string CompanyType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OurCompActivity> Tbl_OurCompActivity { get; set; }
    }
}
