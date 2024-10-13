namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_ProceduresForms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_ProceduresForms()
        {
            Tbl_UsersProcedureForm = new HashSet<Tbl_UsersProcedureForm>();
        }

        public int ID { get; set; }

        public int Form_ID { get; set; }

        public int Procedure_ID { get; set; }

        public virtual Tbl_Forms Tbl_Forms { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_UsersProcedureForm> Tbl_UsersProcedureForm { get; set; }
    }
}
