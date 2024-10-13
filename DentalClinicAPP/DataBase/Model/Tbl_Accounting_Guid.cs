namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Accounting_Guid
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Account_NO { get; set; }

        [Required]
        [StringLength(100)]
        public string Parent_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime From_Date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? To_Date { get; set; }

        public int? AccountsKind_ID { get; set; }

        public int? PersonalAccount { get; set; }

        [StringLength(100)]
        public string Advac_AccountingNO { get; set; }

        public bool? ExpensesAccount { get; set; }

        public bool? HighamountsAccount { get; set; }

        public bool? BrokerAccount { get; set; }

        public bool? ExtrasFinancialYear { get; set; }

        public bool? ElectronicPayments { get; set; }

        public bool? ChequeOut { get; set; }

        public bool? Reciept { get; set; }

        public virtual Tbl_AccountsKind Tbl_AccountsKind { get; set; }
    }
}
