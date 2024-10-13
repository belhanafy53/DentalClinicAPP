

namespace DentalClinicAPP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceLined")]
    public partial class InvoiceLined
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(255)]
        public string itemType { get; set; }

        [StringLength(255)]
        public string itemCode { get; set; }

        [StringLength(255)]
        public string unitType { get; set; }

        public decimal? quantity { get; set; }

        [StringLength(255)]
        public string internalCode { get; set; }

        public decimal? salesTotal { get; set; }

        public decimal? total { get; set; }

        public decimal? valueDifference { get; set; }

        public decimal? totalTaxableFees { get; set; }

        public decimal? netTotal { get; set; }

        public decimal? itemsDiscount { get; set; }

        [StringLength(255)]
        public string unitValue_currencySold { get; set; }

        public decimal? unitValue_amountEGP { get; set; }

        public decimal? discount_rate { get; set; }

        public decimal? discount_amount { get; set; }

        [StringLength(50)]
        public string taxableItems_taxType { get; set; }

        public decimal? taxableItems_amount { get; set; }

        [StringLength(50)]
        public string taxableItems_subType { get; set; }

        public decimal? taxableItems_rate { get; set; }

        public int? InvoiceHeaderID { get; set; }

        public decimal? unitValue_amountSold { get; set; }

        public decimal? unitValue_currencyExchangeRate { get; set; }

        public virtual InvoiceHeader InvoiceHeader { get; set; }
    }
}
