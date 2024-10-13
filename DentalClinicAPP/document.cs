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

    [Table("document")]
    public partial class document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public document()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            Receivers = new HashSet<Receiver>();
            Signatures = new HashSet<Signature>();
        }

        public int ID { get; set; }

        [StringLength(10)]
        public string name { get; set; }

        [StringLength(50)]
        public string documentType { get; set; }

        [StringLength(50)]
        public string documentTypeVersion { get; set; }

        [StringLength(50)]
        public string dateTimeIssued { get; set; }

        [StringLength(50)]
        public string taxpayerActivityCode { get; set; }

        [StringLength(50)]
        public string internalId { get; set; }

        [StringLength(50)]
        public string purchaseOrderReference { get; set; }

        [StringLength(50)]
        public string purchaseOrderDescription { get; set; }

        [StringLength(50)]
        public string salesOrderReference { get; set; }

        [StringLength(50)]
        public string salesOrderDescription { get; set; }

        [StringLength(50)]
        public string proformaInvoiceNumber { get; set; }

        public decimal? totalSalesAmount { get; set; }

        public decimal? totalDiscountAmount { get; set; }

        public decimal? netAmount { get; set; }

        public decimal? extraDiscountAmount { get; set; }

        public decimal? totalItemsDiscountAmount { get; set; }

        public decimal? totalAmount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

        public virtual Issuer Issuer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receiver> Receivers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Signature> Signatures { get; set; }
    }
}
