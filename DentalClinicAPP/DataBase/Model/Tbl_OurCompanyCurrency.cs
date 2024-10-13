namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_OurCompanyCurrency
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        public int? OurCompanyRef { get; set; }
    }
}
