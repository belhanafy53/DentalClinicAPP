namespace DentalClinicAPP.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tbl_Customer
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string NameSectore { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [Required]
        [StringLength(250)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string MotherCompCode { get; set; }

        [StringLength(250)]
        public string MotherCompName { get; set; }

        //[StringLength(50)]
        public int? TaxRecordNo { get; set; }

        [StringLength(30)]
        public string CountryCode { get; set; }

        [StringLength(50)]
        public string Governorate { get; set; }

        [StringLength(150)]
        public string City { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        public int? BuildingNo { get; set; }

        [StringLength(50)]
        public string PostCode { get; set; }

        [StringLength(50)]
        public string SignStar { get; set; }

        public int? GovernerateID { get; set; }

        public int? CityId { get; set; }

        public int? CompanyID { get; set; }

        public int? AccountGuidID { get; set; }

        [StringLength(10)]
        public string CompanyType { get; set; }

        //[StringLength(150)]
        public int? AccountNO { get; set; }
    }
}
