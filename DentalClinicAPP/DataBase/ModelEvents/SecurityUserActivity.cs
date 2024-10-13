namespace DentalClinicAPP.DataBase.ModelEvents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityUserActivity")]
    public partial class SecurityUserActivity
    {
        public long Id { get; set; }

        [StringLength(512)]
        public string Name { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string ManagementName { get; set; }

        [StringLength(150)]
        public string User_systemUnitesName { get; set; }

        public int? User_ID { get; set; }

        public DateTime? LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public TimeSpan? PeriodTime { get; set; }
    }
}
