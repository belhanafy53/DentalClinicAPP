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
    public partial class ErrorDetail
    {
        public int Id { get; set; }

        public int? InvoiceHeaderId { get; set; }

        [StringLength(50)]
        public string InternalId { get; set; }

        public string MessageDetail { get; set; }

        public DateTime? insertDate { get; set; }

        public int? Insertby { get; set; }

        public bool? UpdateFlag { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string target { get; set; }

        public virtual InvoiceHeader InvoiceHeader { get; set; }
    }
}