

namespace DentalClinicAPP
{
    
    using Newtonsoft.Json;
    using OfficeOpenXml.Packaging.Ionic.Zlib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreatJason
    {
        private InvoiceEF _InvoiceEF;
        public class Address
        {
            public string branchID { get; set; }
            public string country { get; set; }
            public string governate { get; set; }
            public string regionCity { get; set; }
            public string street { get; set; }
            public string buildingNumber { get; set; }
        }

        public class Receiver_Address
        {

            public string country { get; set; }
            public string governate { get; set; }
            public string regionCity { get; set; }
            public string street { get; set; }
            public string buildingNumber { get; set; }
        }

        public class Issuer
        {
            public Address address { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Receiver
        {
            public Receiver_Address address { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class UnitValue
        {
            public string currencySold { get; set; }
            public decimal amountEGP { get; set; }
            public decimal amountSold { get; set; }
            public decimal currencyExchangeRate { get; set; }
        }

        public class Discount
        {
            public decimal rate { get; set; }
            public decimal amount { get; set; }
        }

        public class TaxableItem
        {
            public string taxType { get; set; }
            public decimal amount { get; set; }
            public string subType { get; set; }
            public decimal rate { get; set; }
        }

        public class InvoiceLine
        {
            public string description { get; set; }
            public string itemType { get; set; }
            public string itemCode { get; set; }
            public string unitType { get; set; }
            public decimal quantity { get; set; }
            public string internalCode { get; set; }
            public decimal salesTotal { get; set; }
            public decimal total { get; set; }
            public decimal valueDifference { get; set; }
            public decimal totalTaxableFees { get; set; }
            public decimal netTotal { get; set; }
            public decimal itemsDiscount { get; set; }
            public UnitValue unitValue { get; set; }
            public Discount discount { get; set; }
            public List<TaxableItem> taxableItems { get; set; }
        }

        public class TaxTotal
        {
            public string taxType { get; set; }
            public decimal amount { get; set; }
        }

        public class Document
        {
            // public int Id { get; set; }
            public Issuer issuer { get; set; }
            public Receiver receiver { get; set; }
            public string documentType { get; set; }
            public string documentTypeVersion { get; set; }
            public string dateTimeIssued { get; set; }
            public string taxpayerActivityCode { get; set; }
            public string internalID { get; set; }
            public List<InvoiceLine> invoiceLines { get; set; }
            public decimal totalDiscountAmount { get; set; }
            public decimal totalSalesAmount { get; set; }
            public decimal netAmount { get; set; }
            public List<TaxTotal> taxTotals { get; set; }
            public decimal totalAmount { get; set; }
            public decimal extraDiscountAmount { get; set; }
            public decimal totalItemsDiscountAmount { get; set; }
            public List<signatures> signatures { get; set; }
        }

        public class DocumentObj
        {
            // public int Id { get; set; }
            public Issuer issuer { get; set; }
            public Receiver receiver { get; set; }
            public string documentType { get; set; }
            public string documentTypeVersion { get; set; }
            public string dateTimeIssued { get; set; }
            public string taxpayerActivityCode { get; set; }
            public string internalID { get; set; }
            public List<InvoiceLine> invoiceLines { get; set; }
            public decimal totalDiscountAmount { get; set; }
            public decimal totalSalesAmount { get; set; }
            public decimal netAmount { get; set; }
            public List<TaxTotal> taxTotals { get; set; }
            public decimal totalAmount { get; set; }
            public decimal extraDiscountAmount { get; set; }
            public decimal totalItemsDiscountAmount { get; set; }
            // public List<Signature> Signature { get; set; }
        }


        public class Root
        {
            public List<Document> documents { get; set; }
        }
        public class Payment
        {
            public string bankName { get; set; }
            public string bankAddress { get; set; }
            public string bankAccountNo { get; set; }
            public string bankAccountIBAN { get; set; }
            public string swiftCode { get; set; }
            public string terms { get; set; }
        }

        public class Delivery
        {
            public string approach { get; set; }
            public string packaging { get; set; }
            public DateTime? dateValidity { get; set; }
            public string exportPort { get; set; }
            public string countryOfOrigin { get; set; }
            public decimal? grossWeight { get; set; }
            public decimal? netWeight { get; set; }
            public string terms { get; set; }
        }
        public class signatures
        {
            public string signatureType { get; set; }
            public string value { get; set; }
        }


        public string createJson(List<InvoiceHeader> InvoiceHeaderList)
        {
            List<Document> DocumentList = new List<Document>();
            foreach (InvoiceHeader InvoiceHeaderObj in InvoiceHeaderList)
            {
                CreatJason.Document DocumentObj = new Document();
                DocumentObj.documentType = "I";
                DocumentObj.documentTypeVersion = "1.0";
                if (InvoiceHeaderObj.extraDiscountAmount == null)
                    DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.extraDiscountAmount.ToString()), 5);
                DocumentObj.internalID = InvoiceHeaderObj.internalID;
                if (InvoiceHeaderObj.netAmount == null)
                    DocumentObj.netAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.netAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.netAmount.ToString()), 5);
                DocumentObj.taxpayerActivityCode = InvoiceHeaderObj.taxpayerActivityCode;
                if (InvoiceHeaderObj.totalAmount == null)
                    DocumentObj.totalAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.totalAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalAmount.ToString()), 5);
                if (InvoiceHeaderObj.totalDiscountAmount == null)
                    DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalDiscountAmount.ToString()), 5);
                if (InvoiceHeaderObj.totalItemsDiscountAmount == null)
                    DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalItemsDiscountAmount.ToString()), 5);
                if (InvoiceHeaderObj.totalSalesAmount == null)
                    DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse("0"), 5);
                else
                    DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalSalesAmount.ToString()), 5);
                DocumentObj.issuer = GetIssure(InvoiceHeaderObj);
                DocumentObj.receiver = GetReceiver(InvoiceHeaderObj);
                // DocumentObj.payment = GetPayment(invoice);
                // DocumentObj.delivery = GetDelivery(invoice);
                DocumentObj.invoiceLines = GetInvoiceLine(InvoiceHeaderObj);
                DocumentObj.taxTotals = GetTaxTotal(InvoiceHeaderObj);
                DocumentList.Add(DocumentObj);
            }
            Root Rootobj = new Root();
            Rootobj.documents = DocumentList;

            string json = JsonConvert.SerializeObject(Rootobj, Formatting.Indented);
            return json;
        }

        public string createJsonSig(InvoiceHeader InvoiceHeaderObj)
        {
            // List<Document> DocumentList = new List<Document>();

            CreatJason.Document DocumentObj = new Document();
            DocumentObj.dateTimeIssued = DateTime.UtcNow.ToString();
            //DocumentObj.dateTimeIssued = InvoiceHeaderObj.dateTimeIssued;
            DocumentObj.documentType = "I";
            //DocumentObj.documentType = InvoiceHeaderObj.documentType;
            DocumentObj.documentTypeVersion = "1.0";
            //DocumentObj.documentTypeVersion = InvoiceHeaderObj.documentTypeVersion;
            if (InvoiceHeaderObj.extraDiscountAmount == null)
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.extraDiscountAmount.ToString()), 5);
            DocumentObj.internalID = InvoiceHeaderObj.internalID;
            if (InvoiceHeaderObj.netAmount == null)
                DocumentObj.netAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.netAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.netAmount.ToString()), 5);
            DocumentObj.taxpayerActivityCode = InvoiceHeaderObj.taxpayerActivityCode;
            if (InvoiceHeaderObj.totalAmount == null)
                DocumentObj.totalAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalDiscountAmount == null)
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalItemsDiscountAmount == null)
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalItemsDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalSalesAmount == null)
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalSalesAmount.ToString()), 5);
            DocumentObj.issuer = GetIssure(InvoiceHeaderObj);
            DocumentObj.receiver = GetReceiver(InvoiceHeaderObj);
            // DocumentObj.payment = GetPayment(invoice);
            // DocumentObj.delivery = GetDelivery(invoice);
            DocumentObj.invoiceLines = GetInvoiceLine(InvoiceHeaderObj);
            DocumentObj.taxTotals = GetTaxTotal(InvoiceHeaderObj);
            DocumentObj.signatures = GetSignature(InvoiceHeaderObj);
            // DocumentList.Add(DocumentObj);

            //  Root Rootobj = new Root();
            // Rootobj.documents = DocumentList;

            string json = JsonConvert.SerializeObject(DocumentObj, Formatting.Indented);
            return json;
        }

        public DocumentObj CreatObj(InvoiceHeader InvoiceHeaderObj)
        {
            // List<Document> DocumentList = new List<Document>();

            CreatJason.DocumentObj DocumentObj = new DocumentObj();
            DocumentObj.dateTimeIssued = InvoiceHeaderObj.dateTimeIssued;
            DocumentObj.documentType = InvoiceHeaderObj.documentType;
            DocumentObj.documentTypeVersion = InvoiceHeaderObj.documentTypeVersion;
            if (InvoiceHeaderObj.extraDiscountAmount == null)
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.extraDiscountAmount.ToString()), 5);
            DocumentObj.internalID = InvoiceHeaderObj.internalID;
            if (InvoiceHeaderObj.netAmount == null)
                DocumentObj.netAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.netAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.netAmount.ToString()), 5);
            DocumentObj.taxpayerActivityCode = InvoiceHeaderObj.taxpayerActivityCode;
            if (InvoiceHeaderObj.totalAmount == null)
                DocumentObj.totalAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalDiscountAmount == null)
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalItemsDiscountAmount == null)
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalItemsDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalSalesAmount == null)
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalSalesAmount.ToString()), 5);
            DocumentObj.issuer = GetIssure(InvoiceHeaderObj);
            DocumentObj.receiver = GetReceiver(InvoiceHeaderObj);
            // DocumentObj.payment = GetPayment(invoice);
            // DocumentObj.delivery = GetDelivery(invoice);
            DocumentObj.invoiceLines = GetInvoiceLine(InvoiceHeaderObj);
            DocumentObj.taxTotals = GetTaxTotal(InvoiceHeaderObj);
            // DocumentList.Add(DocumentObj);

            //  Root Rootobj = new Root();
            // Rootobj.documents = DocumentList;

            //string json = JsonConvert.SerializeObject(DocumentObj, Formatting.Indented);
            return DocumentObj;
        }

        public string createJsonObj(InvoiceHeader InvoiceHeaderObj)
        {
            // List<Document> DocumentList = new List<Document>();

            CreatJason.DocumentObj DocumentObj = new DocumentObj();
            DocumentObj.dateTimeIssued = InvoiceHeaderObj.dateTimeIssued;
            DocumentObj.documentType = InvoiceHeaderObj.documentType;
            DocumentObj.documentTypeVersion = InvoiceHeaderObj.documentTypeVersion;
            if (InvoiceHeaderObj.extraDiscountAmount == null)
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.extraDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.extraDiscountAmount.ToString()), 5);
            DocumentObj.internalID = InvoiceHeaderObj.internalID;
            if (InvoiceHeaderObj.netAmount == null)
                DocumentObj.netAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.netAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.netAmount.ToString()), 5);
            DocumentObj.taxpayerActivityCode = InvoiceHeaderObj.taxpayerActivityCode;
            if (InvoiceHeaderObj.totalAmount == null)
                DocumentObj.totalAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalDiscountAmount == null)
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalItemsDiscountAmount == null)
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalItemsDiscountAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalItemsDiscountAmount.ToString()), 5);
            if (InvoiceHeaderObj.totalSalesAmount == null)
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse("0"), 5);
            else
                DocumentObj.totalSalesAmount = decimal.Round(decimal.Parse(InvoiceHeaderObj.totalSalesAmount.ToString()), 5);
            DocumentObj.issuer = GetIssure(InvoiceHeaderObj);
            DocumentObj.receiver = GetReceiver(InvoiceHeaderObj);
            // DocumentObj.payment = GetPayment(invoice);
            // DocumentObj.delivery = GetDelivery(invoice);
            DocumentObj.invoiceLines = GetInvoiceLine(InvoiceHeaderObj);
            DocumentObj.taxTotals = GetTaxTotal(InvoiceHeaderObj);
            // DocumentList.Add(DocumentObj);

            //  Root Rootobj = new Root();
            // Rootobj.documents = DocumentList;

            string json = JsonConvert.SerializeObject(DocumentObj, Formatting.Indented);
            return json;
        }


        public Issuer GetIssure(InvoiceHeader InvoiceHeaderObj)
        {
            Issuer IssureObj = new Issuer();



            IssureObj.id = InvoiceHeaderObj.issuer_id;
            IssureObj.name = InvoiceHeaderObj.issuer_name;
            IssureObj.type = InvoiceHeaderObj.issuer_type;
            IssureObj.address = GetIssureAddress(InvoiceHeaderObj);



            return IssureObj;
        }
        public Receiver GetReceiver(InvoiceHeader InvoiceHeaderObj)
        {
            Receiver ReceiverObj = new Receiver();

            ReceiverObj.type = InvoiceHeaderObj.receiver_type;
            ReceiverObj.name = InvoiceHeaderObj.receiver_name;
            ReceiverObj.id = InvoiceHeaderObj.receiver_id;
            ReceiverObj.address = GetReceiverAddress(InvoiceHeaderObj);



            return ReceiverObj;
        }
        public Address GetIssureAddress(InvoiceHeader obj)
        {
            Address IssureObj = new Address();



            // IssureObj.additionalInformation = obj.issuer__address__branchID;
            IssureObj.branchID = obj.issuer_branchID;
            IssureObj.buildingNumber = obj.issuer_buildingNumber;
            IssureObj.country = obj.issuer_country;
            //  IssureObj.floor = obj.issuer__address__floor;
            IssureObj.governate = obj.issuer_governate;
            //   IssureObj.landmark = obj.issuer_landmark;
            //   IssureObj.postalCode = obj.issuer_postalCode;
            IssureObj.regionCity = obj.issuer_regionCity;
            //   IssureObj.room = obj.issuer__address__room;
            IssureObj.street = obj.issuer_street;


            return IssureObj;
        }
        public Receiver_Address GetReceiverAddress(InvoiceHeader obj)
        {
            Receiver_Address IssureObj = new Receiver_Address();


            //IssureObj.additionalInformation = obj.issuer__address__branchID;
            //IssureObj.branchID = obj.issuer__address__branchID;
            IssureObj.buildingNumber = obj.receiver_buildingNumber;
            IssureObj.country = obj.receiver_country;
            //IssureObj.floor = obj.issuer__address__floor;
            IssureObj.governate = obj.receiver_governate;
            //IssureObj.landmark = obj.issuer__address__landmark;
            //IssureObj.postalCode = obj.issuer__address__postalCode;
            IssureObj.regionCity = obj.receiver_regionCity;
            //IssureObj.room = obj.issuer__address__room;
            IssureObj.street = obj.receiver_street;


            return IssureObj;
        }
        public List<InvoiceLine> GetInvoiceLine(InvoiceHeader obj)
        {
            List<InvoiceLine> InvoiceLineList = new List<InvoiceLine>();
            Delivery DeliveryObj = new Delivery();
            List<InvoiceLined> InvoiceLinedList = new List<InvoiceLined>();
            InvoiceLinedDB _InvoiceLinedDB = new InvoiceLinedDB();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceLinedList = _InvoiceLinedDB.GetRecordByInvoiceHeaderID(obj.ID);

                decimal totalSalesAmount = 0;
                decimal totalDiscount = 0;
                decimal totalItemsDiscount = 0;
                decimal ValueaddedTax = 0;
                decimal ExtraInvoiceDiscount = 0;

                foreach (InvoiceLined OBJ in InvoiceLinedList)
                {
                    InvoiceLine InvoiceLineobj = new InvoiceLine();
                    InvoiceLineobj.description = OBJ.description;
                    InvoiceLineobj.internalCode = OBJ.internalCode;
                    if (OBJ.itemCode != null)
                        InvoiceLineobj.itemCode = OBJ.itemCode.ToString();
                    if (OBJ.itemsDiscount != null)
                        InvoiceLineobj.itemsDiscount = decimal.Round(decimal.Parse(OBJ.itemsDiscount.ToString()), 5);
                    else
                        InvoiceLineobj.itemsDiscount = decimal.Round(decimal.Parse("0"), 5);
                    InvoiceLineobj.itemType = OBJ.itemType;
                    if (OBJ.totalTaxableFees != null)
                        InvoiceLineobj.totalTaxableFees = decimal.Round(decimal.Parse(OBJ.totalTaxableFees.ToString()), 5);
                    else
                        InvoiceLineobj.totalTaxableFees = decimal.Round(decimal.Parse("0"), 5);
                    InvoiceLineobj.itemType = OBJ.itemType;
                    if (OBJ.netTotal != null)
                        InvoiceLineobj.netTotal = decimal.Round(decimal.Parse(OBJ.netTotal.ToString()), 5);
                    else
                        InvoiceLineobj.netTotal = decimal.Round(decimal.Parse("0"), 5);
                    if (OBJ.quantity != null)
                        InvoiceLineobj.quantity = decimal.Round(decimal.Parse(OBJ.quantity.ToString()), 5);
                    else
                        InvoiceLineobj.quantity = decimal.Round(decimal.Parse("0"), 5);
                    if (OBJ.salesTotal != null)
                        InvoiceLineobj.salesTotal = decimal.Round(decimal.Parse(OBJ.salesTotal.ToString()), 5);
                    else
                        InvoiceLineobj.salesTotal = decimal.Round(decimal.Parse("0"), 5);
                    if (OBJ.total != null)
                        InvoiceLineobj.total = decimal.Round(decimal.Parse(OBJ.total.ToString()), 5);
                    else
                        InvoiceLineobj.total = decimal.Round(decimal.Parse("0"), 5);
                    InvoiceLineobj.unitType = OBJ.unitType;
                    if (OBJ.valueDifference != null)
                        InvoiceLineobj.valueDifference = decimal.Round(decimal.Parse(OBJ.valueDifference.ToString()), 5);
                    else
                        InvoiceLineobj.valueDifference = decimal.Round(decimal.Parse("0"), 5);
                    InvoiceLineobj.unitValue = GetUnitValue(OBJ);
                    InvoiceLineobj.discount = GetDiscount(OBJ);
                    InvoiceLineobj.taxableItems = GetTaxableItem(OBJ);

                    // جمع salesTotal لكل عنصر
                    totalSalesAmount += InvoiceLineobj.salesTotal;
                    
                     totalItemsDiscount += InvoiceLineobj.itemsDiscount  ;
                     ValueaddedTax += InvoiceLineobj.valueDifference;
                   

                    InvoiceLineList.Add(InvoiceLineobj);
                }

                // تعيين قيمة totalSalesAmount في الكائن obj
                obj.totalSalesAmount = totalSalesAmount;
                obj.totalDiscountAmount = totalItemsDiscount;
                //obj.extraDiscountAmount = totalSalesAmount;
                obj.totalAmount = totalSalesAmount + totalItemsDiscount  ;
            }

            return InvoiceLineList;
        }

        //public List<InvoiceLine> GetInvoiceLine(InvoiceHeader obj)
        //{
        //    List<InvoiceLine> InvoiceLineList = new List<InvoiceLine>();
        //    Delivery DeliveryObj = new Delivery();
        //    List<InvoiceLined> InvoiceLinedList = new List<InvoiceLined>();
        //    InvoiceLinedDB _InvoiceLinedDB = new InvoiceLinedDB();
        //    using (_InvoiceEF = new InvoiceEF())
        //    {
        //        InvoiceLinedList = _InvoiceLinedDB.GetRecordByInvoiceHeaderID(obj.ID);
        //        //var quary = _InvoiceEF.InvoiceLineds
        //        // .Where(x => x.InvoiceHeaderID == obj.ID)
        //        // .Select(m => new
        //        // {
        //        //     m.description,
        //        //     m.internalCode,
        //        //     m.itemCode,
        //        //     m.itemsDiscount,
        //        //     m.itemType,
        //        //     m.totalTaxableFees,
        //        //     m.netTotal,
        //        //     m.quantity,
        //        //     m.salesTotal,
        //        //     m.total,
        //        //     m.unitType,
        //        //     m.valueDifference
        //        // }).Distinct().ToList();

        //        foreach (InvoiceLined OBJ in InvoiceLinedList)
        //        {
        //            InvoiceLine InvoiceLineobj = new InvoiceLine();
        //            InvoiceLineobj.description = OBJ.description;
        //            InvoiceLineobj.internalCode = OBJ.internalCode;
        //            if (OBJ.itemCode != null)
        //                InvoiceLineobj.itemCode = OBJ.itemCode.ToString();
        //            if (OBJ.itemsDiscount != null)
        //                InvoiceLineobj.itemsDiscount = decimal.Round(decimal.Parse(OBJ.itemsDiscount.ToString()), 5);
        //            else
        //                InvoiceLineobj.itemsDiscount = decimal.Round(decimal.Parse("0"), 5);
        //            InvoiceLineobj.itemType = OBJ.itemType;
        //            if (OBJ.totalTaxableFees != null)
        //                InvoiceLineobj.totalTaxableFees = decimal.Round(decimal.Parse(OBJ.totalTaxableFees.ToString()), 5);
        //            else
        //                InvoiceLineobj.totalTaxableFees = decimal.Round(decimal.Parse("0"), 5);
        //            InvoiceLineobj.itemType = OBJ.itemType;
        //            if (OBJ.netTotal != null)
        //                InvoiceLineobj.netTotal = decimal.Round(decimal.Parse(OBJ.netTotal.ToString()), 5);
        //            else
        //                InvoiceLineobj.netTotal = decimal.Round(decimal.Parse("0"), 5);
        //            if (OBJ.quantity != null)
        //                InvoiceLineobj.quantity = decimal.Round(decimal.Parse(OBJ.quantity.ToString()), 5);
        //            else
        //                InvoiceLineobj.quantity = decimal.Round(decimal.Parse("0"), 5);
        //            if (OBJ.salesTotal != null)
        //                InvoiceLineobj.salesTotal = decimal.Round(decimal.Parse(OBJ.salesTotal.ToString()), 5);
        //            else
        //                InvoiceLineobj.salesTotal = decimal.Round(decimal.Parse("0"), 5);
        //            if (OBJ.total != null)
        //                InvoiceLineobj.total = decimal.Round(decimal.Parse(OBJ.total.ToString()), 5);
        //            else
        //                InvoiceLineobj.total = decimal.Round(decimal.Parse("0"), 5);
        //            InvoiceLineobj.unitType = OBJ.unitType;
        //            if (OBJ.valueDifference != null)
        //                InvoiceLineobj.valueDifference = decimal.Round(decimal.Parse(OBJ.valueDifference.ToString()), 5);
        //            else
        //                InvoiceLineobj.valueDifference = decimal.Round(decimal.Parse("0"), 5);
        //            InvoiceLineobj.unitValue = GetUnitValue(OBJ);
        //            InvoiceLineobj.discount = GetDiscount(OBJ);
        //            InvoiceLineobj.taxableItems = GetTaxableItem(OBJ);
        //            InvoiceLineList.Add(InvoiceLineobj);
        //        }

        //    }

        //    return InvoiceLineList;
        //}
        public UnitValue GetUnitValue(InvoiceLined obj)
        {
            UnitValue UnitValueObj = new UnitValue();


            if (obj.unitValue_amountEGP != null)
                UnitValueObj.amountEGP = decimal.Round(decimal.Parse(obj.unitValue_amountEGP.ToString()), 5);
            else
                UnitValueObj.amountEGP = decimal.Round(decimal.Parse("0"), 5);
            if (obj.unitValue_amountSold != null)
                UnitValueObj.amountSold = decimal.Round(decimal.Parse(obj.unitValue_amountSold.ToString()), 5);
            else
                UnitValueObj.amountSold = decimal.Round(decimal.Parse("0"), 5);
            if (obj.unitValue_currencyExchangeRate != null)
                UnitValueObj.currencyExchangeRate = decimal.Round(decimal.Parse(obj.unitValue_currencyExchangeRate.ToString()), 5);
            else
                UnitValueObj.currencyExchangeRate = decimal.Round(decimal.Parse("0"), 5);
            UnitValueObj.currencySold = obj.unitValue_currencySold;

            return UnitValueObj;

        }
       
        public Discount GetDiscount(InvoiceLined obj)
        {
            Discount DiscountObj = new Discount();

            //if (obj.discount_amount != null)
            //    DiscountObj.amount = decimal.Round(decimal.Parse(obj.discount_amount.ToString()), 5);
            //else
                DiscountObj.amount = decimal.Round(decimal.Parse("0"), 5);
            //if (obj.discount_rate != null)
                DiscountObj.rate = decimal.Round(decimal.Parse("0"), 5);

            return DiscountObj;
        }

        public List<TaxableItem> GetTaxableItem(InvoiceLined obj)
        {
            List<TaxableItem> TaxableItemList = new List<TaxableItem>();


            TaxableItem TaxableItemObj = new TaxableItem();
            if (obj.taxableItems_amount != null)
                TaxableItemObj.amount = decimal.Round(decimal.Parse(obj.taxableItems_amount.ToString()), 5);
            else
                TaxableItemObj.amount = decimal.Round(decimal.Parse("0"), 5);
            if (obj.taxableItems_rate != null)
                TaxableItemObj.rate = decimal.Round(decimal.Parse(obj.taxableItems_rate.ToString()), 2);
            else
                TaxableItemObj.rate = decimal.Round(decimal.Parse("0"), 2);
            TaxableItemObj.subType = obj.taxableItems_subType;
            TaxableItemObj.taxType = obj.taxableItems_taxType;
            TaxableItemList.Add(TaxableItemObj);


            return TaxableItemList;
        }
        public List<TaxTotal> GetTaxTotal(InvoiceHeader obj)
        {
            List<TaxTotal> TaxTotalList = new List<TaxTotal>();

            TaxTotal TaxTotalObj = new TaxTotal();
            if (obj.taxTotals__amount != null)
                TaxTotalObj.amount = decimal.Round(decimal.Parse(obj.taxTotals__amount.ToString()), 5);
            else
                TaxTotalObj.amount = decimal.Round(decimal.Parse("0"), 5);
            TaxTotalObj.taxType = obj.taxTotals__taxType;
            TaxTotalList.Add(TaxTotalObj);

            return TaxTotalList;

        }
        public List<signatures> GetSignature(InvoiceHeader obj)
        {
            List<signatures> SignatureList = new List<signatures>();

            signatures SignatureObj = new signatures();
            SignatureObj.signatureType = obj.signatureType;
            SignatureObj.value = obj.signaturevalue;
            SignatureList.Add(SignatureObj);

            return SignatureList;

        }

        public string DisplayDateWithTimeZoneName(DateTime date1, TimeZoneInfo timeZone)
        {
            string datezone = timeZone.IsDaylightSavingTime(date1) ? timeZone.DaylightName : timeZone.StandardName;
            return datezone;
        }
    }
}
