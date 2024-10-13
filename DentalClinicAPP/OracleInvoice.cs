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

   
    public partial class OracleInvoice
    {
        public int ID { get; set; }

        
        public string invoice_id { get; set; }

       
        public string issuer__address__branchID { get; set; }

      
        public string issuer__address__country { get; set; }

        
        public string issuer__address__governate { get; set; }

        
        public string issuer__address__regionCity { get; set; }

       
        public string issuer__address__street { get; set; }

        
        public string issuer__address__buildingNumber { get; set; }

       
        public string issuer__type { get; set; }

      
        public string issuer__id { get; set; }

       
        public string issuer__name { get; set; }

      
        public string receiver__address__country { get; set; }

        
        public string receiver__address__governate { get; set; }

        
        public string receiver__address__regionCity { get; set; }

       
        public string receiver__address__street { get; set; }

       
        public string receiver__address__buildingNumber { get; set; }

       
        public string receiver__type { get; set; }

       
        public string receiver__id { get; set; }

       
        public string receiver__name { get; set; }

       
        public string documentType { get; set; }

        
        public string documentTypeVersion { get; set; }

       
        public string dateTimeIssued { get; set; }

        
        public string taxpayerActivityCode { get; set; }

        
        public string internalID { get; set; }

       
        public string invoiceLines__description { get; set; }

        
        public string invoiceLines__itemType { get; set; }

       
        public string invoiceLines__itemCode { get; set; }

      
        public string invoiceLines__unitType { get; set; }

        public decimal? invoiceLines__quantity { get; set; }

        
        public string invoiceLines__internalCode { get; set; }

        public decimal? invoiceLines__salesTotal { get; set; }

        public decimal? invoiceLines__total { get; set; }

        public decimal? invoiceLines__valueDifference { get; set; }

        public decimal? invoiceLines__totalTaxableFees { get; set; }

        public decimal? invoiceLines__netTotal { get; set; }

        public decimal? invoiceLines__itemsDiscount { get; set; }

        
        public string invoiceLines__unitValue__currencySold { get; set; }

        public decimal? invoiceLines__unitValue__amountEGP { get; set; }

        public decimal? totalDiscountAmount { get; set; }

        public decimal? totalSalesAmount { get; set; }

        public decimal? netAmount { get; set; }

        
        public string taxTotals__taxType { get; set; }

        public decimal? taxTotals__amount { get; set; }

        public decimal? totalAmount { get; set; }

        public decimal? extraDiscountAmount { get; set; }

        public decimal? totalItemsDiscountAmount { get; set; }

        public decimal? invoiceLines__discount__rate { get; set; }

        public decimal? invoiceLines__discount__amount { get; set; }

       
        public string invoiceLines__taxableItems__taxType { get; set; }

        public decimal? invoiceLines__taxableItems__amount { get; set; }

       
        public string invoiceLines__taxableItems__subType { get; set; }

        public decimal? invoiceLines__taxableItems__rate { get; set; }

        public int? SendStatusId { get; set; }

       
        public string submissionID { get; set; }

        
        public string longId { get; set; }

      
        public string hashKey { get; set; }

       
        public string errorCode { get; set; }

       
        public string erroeMessage { get; set; }

      
        public string errorTarget { get; set; }

      
        public string uuid { get; set; }

        
        public string errordetailsTargt { get; set; }

        public string errordetailsmessage { get; set; }

        
        public string signatureType { get; set; }

        public string signaturevalue { get; set; }

        public virtual SendStatu SendStatu { get; set; }
    }
}
