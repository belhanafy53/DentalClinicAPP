namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Activities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Activities()
        {
            Tbl_OurCompActivity = new HashSet<Tbl_OurCompActivity>();
        }

        [Key]
        [StringLength(50)]
        public string Activecode { get; set; }

        [StringLength(250)]
        public string dec_en { get; set; }

        [StringLength(250)]
        public string dec_ar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OurCompActivity> Tbl_OurCompActivity { get; set; }
    }
}
