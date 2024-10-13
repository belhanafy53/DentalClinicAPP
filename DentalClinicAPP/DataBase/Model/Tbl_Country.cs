namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Country
    {
        [Required]
        [StringLength(30)]
        public string code { get; set; }

        [StringLength(250)]
        public string dec_en { get; set; }

        [StringLength(250)]
        public string dec_ar { get; set; }

        public int id { get; set; }
    }
}
