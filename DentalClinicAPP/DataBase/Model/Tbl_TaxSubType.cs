namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_TaxSubType
    {
        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string Desc_En { get; set; }

        [StringLength(250)]
        public string Desc_Ar { get; set; }

        [StringLength(50)]
        public string TaxTypeRefrence { get; set; }

        public virtual Tbl_TaxType Tbl_TaxType { get; set; }
    }
}
