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
    public partial class InvoiceLine_TaxableItem
    {
        public int Id { get; set; }

        public int? InvoiceLineID { get; set; }

        public int? TaxableId { get; set; }

        public virtual InvoiceLine InvoiceLine { get; set; }

        public virtual TaxableItem TaxableItem { get; set; }
    }
}
