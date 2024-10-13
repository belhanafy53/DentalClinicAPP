﻿using System;
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

    [Table("InvoiceHeadeStr")]
    public partial class InvoiceHeadeStr
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvoiceHeadeStr()
        {
            InvoiceLinedstrs = new HashSet<InvoiceLinedstr>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string issuer_branchID { get; set; }

        [StringLength(255)]
        public string issuer_country { get; set; }

        [StringLength(255)]
        public string issuer_governate { get; set; }

        [StringLength(255)]
        public string issuer_regionCity { get; set; }

        [StringLength(255)]
        public string issuer_street { get; set; }

        [StringLength(255)]
        public string issuer_buildingNumber { get; set; }

        [StringLength(255)]
        public string issuer_type { get; set; }

        [StringLength(50)]
        public string issuer_id { get; set; }

        [StringLength(255)]
        public string issuer_name { get; set; }

        [StringLength(255)]
        public string receiver_country { get; set; }

        [StringLength(255)]
        public string receiver_governate { get; set; }

        [StringLength(255)]
        public string receiver_regionCity { get; set; }

        [StringLength(255)]
        public string receiver_street { get; set; }

        [StringLength(255)]
        public string receiver_buildingNumber { get; set; }

        [StringLength(255)]
        public string receiver_type { get; set; }

        [StringLength(50)]
        public string receiver_id { get; set; }

        [StringLength(255)]
        public string receiver_name { get; set; }

        [StringLength(255)]
        public string documentType { get; set; }

        [StringLength(50)]
        public string documentTypeVersion { get; set; }

        [StringLength(50)]
        public string dateTimeIssued { get; set; }

        [StringLength(255)]
        public string taxpayerActivityCode { get; set; }

        [StringLength(255)]
        public string internalID { get; set; }

        [StringLength(255)]
        public string totalDiscountAmount { get; set; }

        [StringLength(255)]
        public string totalSalesAmount { get; set; }

        [StringLength(255)]
        public string netAmount { get; set; }

        [StringLength(255)]
        public string taxTotals__taxType { get; set; }

        [StringLength(255)]
        public string taxTotals__amount { get; set; }

        [StringLength(255)]
        public string totalAmount { get; set; }

        [StringLength(255)]
        public string extraDiscountAmount { get; set; }

        [StringLength(255)]
        public string totalItemsDiscountAmount { get; set; }

        public int? SendStatusId { get; set; }

        [StringLength(255)]
        public string submissionID { get; set; }

        [StringLength(255)]
        public string longId { get; set; }

        [StringLength(255)]
        public string hashKey { get; set; }

        [StringLength(255)]
        public string errorCode { get; set; }

        [StringLength(255)]
        public string erroeMessage { get; set; }

        [StringLength(255)]
        public string errorTarget { get; set; }

        [StringLength(255)]
        public string uuid { get; set; }

        [StringLength(255)]
        public string errordetailsTargt { get; set; }

        public string errordetailsmessage { get; set; }

        [StringLength(50)]
        public string signatureType { get; set; }

        public string signaturevalue { get; set; }

        public virtual SendStatu SendStatu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLinedstr> InvoiceLinedstrs { get; set; }
    }
}