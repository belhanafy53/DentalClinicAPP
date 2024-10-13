namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_MenuProgUnit_SysUnites
    {
        public int ID { get; set; }

        public int SysUnites_ID { get; set; }

        public int MenuProgUnit_ID { get; set; }

        public virtual Tbl_SystemUnites Tbl_SystemUnites { get; set; }

        public virtual Tbl_SystemUnites Tbl_SystemUnites1 { get; set; }
    }
}
