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

    public partial class Receiver_Address
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string branchID { get; set; }

        [StringLength(50)]
        public string country { get; set; }

        [StringLength(50)]
        public string governate { get; set; }

        [StringLength(50)]
        public string regionCity { get; set; }

        [StringLength(50)]
        public string street { get; set; }

        [StringLength(50)]
        public string buildingNumber { get; set; }

        [StringLength(50)]
        public string postalCode { get; set; }

        [StringLength(50)]
        public string floor { get; set; }

        [StringLength(50)]
        public string room { get; set; }

        [StringLength(50)]
        public string landmark { get; set; }

        [StringLength(50)]
        public string additionalInformation { get; set; }

        public int? ReceiverID { get; set; }

        public virtual Receiver Receiver { get; set; }
    }
}
