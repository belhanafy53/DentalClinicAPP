namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_OurCompActivity
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ActivityCode { get; set; }

        public int? OurCompanyRef { get; set; }

        public virtual Tbl_Activities Tbl_Activities { get; set; }

        public virtual Tbl_OurCompany Tbl_OurCompany { get; set; }
    }
}
