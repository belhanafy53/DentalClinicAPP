using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("InvoiceLinedstr")]
    public partial class InvoiceLinedstr
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string itemType { get; set; }

        [StringLength(255)]
        public string itemCode { get; set; }

        [StringLength(255)]
        public string unitType { get; set; }

        [StringLength(255)]
        public string quantity { get; set; }

        [StringLength(255)]
        public string internalCode { get; set; }

        [StringLength(255)]
        public string salesTotal { get; set; }

        [StringLength(255)]
        public string total { get; set; }

        [StringLength(255)]
        public string valueDifference { get; set; }

        [StringLength(255)]
        public string totalTaxableFees { get; set; }

        [StringLength(255)]
        public string netTotal { get; set; }

        [StringLength(255)]
        public string itemsDiscount { get; set; }

        [StringLength(255)]
        public string unitValue_currencySold { get; set; }

        [StringLength(255)]
        public string unitValue_amountEGP { get; set; }

        [StringLength(255)]
        public string discount_rate { get; set; }

        [StringLength(255)]
        public string discount_amount { get; set; }

        [StringLength(50)]
        public string taxableItems_taxType { get; set; }

        [StringLength(255)]
        public string taxableItems_amount { get; set; }

        [StringLength(50)]
        public string taxableItems_subType { get; set; }

        [StringLength(255)]
        public string taxableItems_rate { get; set; }

        public int? InvoiceHeaderID { get; set; }

        public virtual InvoiceHeadeStr InvoiceHeadeStr { get; set; }
    }
}
