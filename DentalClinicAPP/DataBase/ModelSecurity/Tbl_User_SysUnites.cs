namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_User_SysUnites
    {
        public int ID { get; set; }

        public int SysUnites_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? From_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }

        public int? User_ID { get; set; }

        public int? SysUnite_StatusID { get; set; }

        public int? Emp_ID { get; set; }

        public virtual Tbl_Employee Tbl_Employee { get; set; }

        public virtual Tbl_SystemUnites Tbl_SystemUnites { get; set; }

        public virtual Tbl_User Tbl_User { get; set; }
    }
}
