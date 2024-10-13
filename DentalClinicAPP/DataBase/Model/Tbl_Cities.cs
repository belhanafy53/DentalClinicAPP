namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Cities
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name_E { get; set; }

        public int GovernorateID { get; set; }

        [StringLength(250)]
        public string Name_Ar { get; set; }

        public virtual Tbl_Governerate Tbl_Governerate { get; set; }
    }
}
