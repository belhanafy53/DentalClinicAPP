namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_TaxType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_TaxType()
        {
            Tbl_TaxSubType = new HashSet<Tbl_TaxSubType>();
        }

        [Key]
        [StringLength(50)]
        public string code { get; set; }

        [StringLength(150)]
        public string Des_En { get; set; }

        [StringLength(150)]
        public string Des_Ar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_TaxSubType> Tbl_TaxSubType { get; set; }
    }
}
