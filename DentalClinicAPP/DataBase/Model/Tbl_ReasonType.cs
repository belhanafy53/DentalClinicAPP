namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_ReasonType
    {
        [Key]
        [StringLength(50)]
        public string code { get; set; }

        [StringLength(150)]
        public string Des_En { get; set; }

        [StringLength(150)]
        public string Des_Ar { get; set; }
    }
}
