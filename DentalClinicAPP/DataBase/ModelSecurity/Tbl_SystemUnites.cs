namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_SystemUnites
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_SystemUnites()
        {
            Tbl_MenuProgUnit_SysUnites = new HashSet<Tbl_MenuProgUnit_SysUnites>();
            Tbl_MenuProgUnit_SysUnites1 = new HashSet<Tbl_MenuProgUnit_SysUnites>();
            Tbl_User_SysUnites = new HashSet<Tbl_User_SysUnites>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_MenuProgUnit_SysUnites> Tbl_MenuProgUnit_SysUnites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_MenuProgUnit_SysUnites> Tbl_MenuProgUnit_SysUnites1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_User_SysUnites> Tbl_User_SysUnites { get; set; }
    }
}
