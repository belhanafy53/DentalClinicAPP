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

    [Table("TaxableItem")]
    public partial class TaxableItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaxableItem()
        {
            InvoiceLine_TaxableItem = new HashSet<InvoiceLine_TaxableItem>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string taxType { get; set; }

        public decimal? amount { get; set; }

        [StringLength(50)]
        public string subType { get; set; }

        public decimal? rate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine_TaxableItem> InvoiceLine_TaxableItem { get; set; }
    }
}
