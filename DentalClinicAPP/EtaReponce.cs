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

    [Table("EtaReponce")]
    public partial class EtaReponce
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string InternalId { get; set; }

        public int? SendStatus { get; set; }

        public string Responce { get; set; }
    }
}
