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
    [Table("Delivery")]

    public partial class Delivery
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string approach { get; set; }

        [StringLength(255)]
        public string packaging { get; set; }

        public DateTime? dateValidity { get; set; }

        [StringLength(50)]
        public string exportPort { get; set; }

        [StringLength(50)]
        public string countryOfOrigin { get; set; }

        public decimal? grossWeight { get; set; }

        public decimal? netWeight { get; set; }

        [StringLength(50)]
        public string terms { get; set; }
    }

}
