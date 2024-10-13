namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_User()
        {
            Tbl_User_SysUnites = new HashSet<Tbl_User_SysUnites>();
            Tbl_UserAuthForms = new HashSet<Tbl_UserAuthForms>();
            Tbl_UsersProcedureForm = new HashSet<Tbl_UsersProcedureForm>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string ImagePath { get; set; }

        [StringLength(50)]
        public string NationalId { get; set; }

        public int? UserType_ID { get; set; }

        public int? UserStatus_ID { get; set; }

        [StringLength(150)]
        public string UserName { get; set; }

        public int? Employee_id { get; set; }

        public virtual Tbl_Employee Tbl_Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_User_SysUnites> Tbl_User_SysUnites { get; set; }

        public virtual Tbl_UserStatus Tbl_UserStatus { get; set; }

        public virtual Tbl_UserType Tbl_UserType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UserAuthForms> Tbl_UserAuthForms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UsersProcedureForm> Tbl_UsersProcedureForm { get; set; }
    }
}
