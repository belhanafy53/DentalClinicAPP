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


    [Table("UserGroup")]
    public partial class UserGroup
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public string Url { get; set; }

        public string LoginUrl { get; set; }

        public string Client_id { get; set; }

        public string Client_secret { get; set; }
    }
}
