namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_ItemsOfTax
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CodeType { get; set; }

        [StringLength(100)]
        public string UniqueNumber { get; set; }

        [StringLength(250)]
        public string CodeName { get; set; }
    }
}
