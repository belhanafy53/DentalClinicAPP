namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_TaxOurCompany
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string TaxTypeCode { get; set; }

        [StringLength(50)]
        public string TaxSubTypeCode { get; set; }

        public int? OurCompanyRef { get; set; }

        public decimal? TaxRate { get; set; }
    }
}
