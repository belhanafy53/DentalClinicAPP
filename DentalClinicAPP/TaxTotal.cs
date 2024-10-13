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


    [Table("TaxTotal")]
    public partial class TaxTotal
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string taxType { get; set; }

        public decimal? amount { get; set; }
    }
}
