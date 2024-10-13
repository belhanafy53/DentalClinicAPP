namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Management
    {
        public int ID { get; set; }

        public int Management_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int? ExchangeCenter_ID { get; set; }

        public int? Parent_ID { get; set; }

        public int? ManagementCategory_ID { get; set; }

        [StringLength(100)]
        public string BrancheName { get; set; }

        public short? KindBranchDirect { get; set; }

        public int? AccountGuidID { get; set; }

        public virtual Tbl_ManagementCategory Tbl_ManagementCategory { get; set; }
    }
}
