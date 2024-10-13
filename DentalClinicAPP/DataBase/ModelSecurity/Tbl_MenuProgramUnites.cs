namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_MenuProgramUnites
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public int? Parent_ID { get; set; }

        public int? Forms_ID { get; set; }

        [StringLength(250)]
        public string Name_Ar { get; set; }
    }
}
