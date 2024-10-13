namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_UsersProcedureForm
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int ProceduresForms_ID { get; set; }

        public virtual Tbl_ProceduresForms Tbl_ProceduresForms { get; set; }

        public virtual Tbl_User Tbl_User { get; set; }
    }
}
