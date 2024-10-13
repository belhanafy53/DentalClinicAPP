namespace DentalClinicAPP.DataBase.ModelSecurity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_UserAuthForms
    {
        public int ID { get; set; }

        public int User_ID { get; set; }

        public int MenuProgUnit_ID { get; set; }

        public int SysUnites_ID { get; set; }

        public int UserType_ID { get; set; }

        public virtual Tbl_User Tbl_User { get; set; }

        public virtual Tbl_UserType Tbl_UserType { get; set; }
    }
}
