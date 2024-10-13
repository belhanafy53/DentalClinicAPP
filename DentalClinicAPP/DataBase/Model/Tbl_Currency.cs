namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Currency
    {
        [Key]
        [StringLength(30)]
        public string code { get; set; }

        [StringLength(150)]
        public string dec_en { get; set; }
    }
}
