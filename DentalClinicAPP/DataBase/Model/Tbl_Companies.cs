namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Companies
    {
        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name_E { get; set; }

        [StringLength(150)]
        public string Name_Ar { get; set; }
    }
}
