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


    [Table("UnitValue")]
    public partial class UnitValue
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string currencySold { get; set; }

        public decimal? amountEGP { get; set; }
    }
}
