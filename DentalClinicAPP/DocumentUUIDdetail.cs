using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class DocumentUUIDdetail
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class InnerError
        {
            public string propertyName { get; set; }
            public string propertyPath { get; set; }
            public string errorCode { get; set; }
            public string error { get; set; }
            public object innerError { get; set; }
        }

        public class Error
        {
            public object propertyName { get; set; }
            public object propertyPath { get; set; }
            public string errorCode { get; set; }
            public string error { get; set; }
            public List<InnerError> innerError { get; set; }
        }

        public class ValidationStep
        {
            public string status { get; set; }
            public Error error { get; set; }
            public string stepName { get; set; }
            public string stepId { get; set; }
        }

        public class ValidationResults
        {
            public string status { get; set; }
            public List<ValidationStep> validationSteps { get; set; }
        }

        public class TaxTotal
        {
            public string taxType { get; set; }
            public double amount { get; set; }
        }

        public class DiscountForeign
        {
            public double amountForeign { get; set; }
            public double rate { get; set; }
            public double amount { get; set; }
        }

        public class LineTaxableItem
        {
            public double amountForeign { get; set; }
            public string taxType { get; set; }
            public double amount { get; set; }
            public string subType { get; set; }
            public double rate { get; set; }
        }

        public class UnitValue
        {
            public string currencySold { get; set; }
            public double amountSold { get; set; }
            public double amountEGP { get; set; }
            public double currencyExchangeRate { get; set; }
        }

        public class Discount
        {
            public double rate { get; set; }
            public double amount { get; set; }
        }

        public class InvoiceLine
        {
            public string itemPrimaryName { get; set; }
            public string itemPrimaryDescription { get; set; }
            public string itemSecondaryName { get; set; }
            public string itemSecondaryDescription { get; set; }
            public string unitTypePrimaryName { get; set; }
            public string unitTypePrimaryDescription { get; set; }
            public string unitTypeSecondaryName { get; set; }
            public string unitTypeSecondaryDescription { get; set; }
            public double salesTotalForeign { get; set; }
            public double netTotalForeign { get; set; }
            public double totalForeign { get; set; }
            public double totalTaxableFeesForeign { get; set; }
            public double itemsDiscountForeign { get; set; }
            public double valueDifferenceForeign { get; set; }
            public DiscountForeign discountForeign { get; set; }
            public List<LineTaxableItem> lineTaxableItems { get; set; }
            public string description { get; set; }
            public string itemType { get; set; }
            public string itemCode { get; set; }
            public string unitType { get; set; }
            public double quantity { get; set; }
            public UnitValue unitValue { get; set; }
            public object factoryUnitValue { get; set; }
            public double salesTotal { get; set; }
            public Discount discount { get; set; }
            public object taxableItems { get; set; }
            public string internalCode { get; set; }
            public double itemsDiscount { get; set; }
            public double netTotal { get; set; }
            public double totalTaxableFees { get; set; }
            public double valueDifference { get; set; }
            public double total { get; set; }
        }

        public class Signature
        {
            public string signatureType { get; set; }
            public string value { get; set; }
            public string signedBy { get; set; }
        }

        public class Address
        {
            public string buildingNumber { get; set; }
            public object room { get; set; }
            public object floor { get; set; }
            public string street { get; set; }
            public object landmark { get; set; }
            public object additionalInformation { get; set; }
            public string governate { get; set; }
            public string regionCity { get; set; }
            public object postalCode { get; set; }
            public string country { get; set; }
            public object branchID { get; set; }
        }

        public class Receiver
        {
            public Address address { get; set; }
            public int type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Issuer
        {
            public Address address { get; set; }
            public int type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class CurrencySegment
        {
            public string currency { get; set; }
            public double currencyExchangeRate { get; set; }
            public double totalItemsDiscountAmount { get; set; }
            public double totalAmount { get; set; }
            public List<TaxTotal> taxTotals { get; set; }
            public double netAmount { get; set; }
            public double totalDiscount { get; set; }
            public double totalSales { get; set; }
            public double extraDiscountAmount { get; set; }
            public double totalTaxableFees { get; set; }
        }

        public class Root
        {
            public string submissionUUID { get; set; }
            public DateTime dateTimeRecevied { get; set; }
            public ValidationResults validationResults { get; set; }
            public string transformationStatus { get; set; }
            public int statusId { get; set; }
            public string status { get; set; }
            public string documentStatusReason { get; set; }
            public object cancelRequestDate { get; set; }
            public object rejectRequestDate { get; set; }
            public object cancelRequestDelayedDate { get; set; }
            public object rejectRequestDelayedDate { get; set; }
            public object declineCancelRequestDate { get; set; }
            public object declineRejectRequestDate { get; set; }
            public DateTime canbeCancelledUntil { get; set; }
            public DateTime canbeRejectedUntil { get; set; }
            public string uuid { get; set; }
            public string publicUrl { get; set; }
            public object purchaseOrderDescription { get; set; }
            public double totalItemsDiscountAmount { get; set; }
            public object delivery { get; set; }
            public object payment { get; set; }
            public double totalAmount { get; set; }
            public List<TaxTotal> taxTotals { get; set; }
            public double netAmount { get; set; }
            public double totalDiscount { get; set; }
            public double totalSales { get; set; }
            public List<InvoiceLine> invoiceLines { get; set; }
            public object references { get; set; }
            public object salesOrderDescription { get; set; }
            public object salesOrderReference { get; set; }
            public object proformaInvoiceNumber { get; set; }
            public List<Signature> signatures { get; set; }
            public object purchaseOrderReference { get; set; }
            public string internalID { get; set; }
            public string taxpayerActivityCode { get; set; }
            public DateTime dateTimeIssued { get; set; }
            public string documentTypeVersion { get; set; }
            public string documentType { get; set; }
            public string documentTypeNamePrimaryLang { get; set; }
            public string documentTypeNameSecondaryLang { get; set; }
            public Receiver receiver { get; set; }
            public Issuer issuer { get; set; }
            public double extraDiscountAmount { get; set; }
            public int maxPercision { get; set; }
            public string currenciesSold { get; set; }
            public List<CurrencySegment> currencySegments { get; set; }
        }
    }
}
