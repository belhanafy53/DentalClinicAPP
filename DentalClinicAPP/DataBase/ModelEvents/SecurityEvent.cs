namespace DentalClinicAPP.DataBase.ModelEvents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SecurityEvent")]
    public partial class SecurityEvent
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(250)]
        public string TableName { get; set; }

        [StringLength(256)]
        public string TableRecordId { get; set; }

        public string Description { get; set; }

        [StringLength(256)]
        public string ManagementName { get; set; }

        public DateTime? ActionDate { get; set; }

        [StringLength(512)]
        public string EmployeeName { get; set; }

        public int? User_ID { get; set; }

        public long? Employee_ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(512)]
        public string FormName { get; set; }

        [Column(TypeName = "ntext")]
        public string ObjectData { get; set; }
    }
}
