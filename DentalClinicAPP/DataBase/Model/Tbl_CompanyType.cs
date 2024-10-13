namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_CompanyType
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string Name_E { get; set; }

        [StringLength(20)]
        public string Name_Ar { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}
