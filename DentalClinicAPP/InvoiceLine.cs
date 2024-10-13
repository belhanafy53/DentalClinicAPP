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

    [Table("InvoiceLine")]
    public partial class InvoiceLine
    {
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceLine()
        {
            InvoiceLine_TaxableItem = new HashSet<InvoiceLine_TaxableItem>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string itemType { get; set; }

        [StringLength(255)]
        public string itemCode { get; set; }

        [StringLength(50)]
        public string unitType { get; set; }

        public decimal? quantity { get; set; }

        [StringLength(50)]
        public string internalCode { get; set; }

        public decimal? salesTotal { get; set; }

        public decimal? total { get; set; }

        public decimal? valueDifference { get; set; }

        public decimal? totalTaxableFees { get; set; }

        public decimal? netTotal { get; set; }

        public decimal? itemsDiscount { get; set; }

        public int? DiscountId { get; set; }

        public int? DocumentId { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual document document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine_TaxableItem> InvoiceLine_TaxableItem { get; set; }
    }
}
