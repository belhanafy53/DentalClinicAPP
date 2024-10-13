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


    [Table("Payment")]
    public partial class Payment
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string bankName { get; set; }

        [StringLength(255)]
        public string bankAddress { get; set; }

        [StringLength(255)]
        public string bankAccountNo { get; set; }

        [StringLength(255)]
        public string bankAccountIBAN { get; set; }

        [StringLength(255)]
        public string swiftCode { get; set; }

        [StringLength(255)]
        public string terms { get; set; }
    }
}
