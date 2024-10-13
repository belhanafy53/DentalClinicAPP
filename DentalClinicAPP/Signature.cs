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


    [Table("Signature")]
    public partial class Signature
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public string value { get; set; }

        public int? DocumentId { get; set; }

        public virtual document document { get; set; }
    }
}
