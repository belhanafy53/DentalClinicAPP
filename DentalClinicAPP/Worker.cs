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


    [Table("Worker")]
    public partial class Worker
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? WorkerId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        public int? MangmentId { get; set; }

        public int? UserGroupId { get; set; }

        public int? RoleId { get; set; }

        public bool? IsDelete { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual Role Role { get; set; }
    }
}
