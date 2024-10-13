using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class OracleInvoiceDB
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

        public class Issuer
        {
            public Address address { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Receiver
        {
            public Address address { get; set; }
            public string type { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }

        public class UnitValue
        {
            public string currencySold { get; set; }
            public decimal amountEGP { get; set; }
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
            //  public List<Signature> Signature { get; set; }
        }

        public class DocumentID
        {
            public int Id { get; set; }
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
        public class Signature
        {
            public string Type { get; set; }
            public string value { get; set; }
        }















        public int SaveRecord(OracleInvoice record)
        {
            using (_InvoiceEF = new InvoiceEF())
            {
                //if (record == 0)
                //{
                //    record.DeviceId = null;
                //}
                //if (record.SecurityUserId == 0)
                //{
                //    record.SecurityUserId = null;
                //}
                //if (record.CodingAddressId == 0)
                //{
                //    record.CodingAddressId = null;
                //}


                //if (record.CodingMangmentId == 0)
                //{
                //    record.CodingMangmentId = null;
                //}



                _InvoiceEF.OracleInvoices.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.ID;
        }

        public bool DeleteRecord(string invoice)
        {

            List<OracleInvoice> _OracleInvoiceList = new List<OracleInvoice>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices.Where(x => x.internalID == invoice);
                _OracleInvoiceList = obj.ToList();
                foreach (OracleInvoice OracleInvoiceobj in _OracleInvoiceList)
                {
                    _InvoiceEF.OracleInvoices.Remove(OracleInvoiceobj);
                    _InvoiceEF.SaveChanges();
                }
                return true;

            }
        }

        public bool UpdateRecordByInternalId(string invoice, int sendstatus,CustomResponse CustomResponseObj)
        {

            List<OracleInvoice> _OracleInvoiceList = new List<OracleInvoice>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices.Where(x => x.internalID == invoice);
                _OracleInvoiceList = obj.ToList();
                foreach (OracleInvoice OracleInvoiceobj in _OracleInvoiceList)
                {
                    OracleInvoiceobj.SendStatusId = int.Parse(sendstatus.ToString());
                    OracleInvoiceobj.submissionID = CustomResponseObj.submissionID;
                    OracleInvoiceobj.uuid = CustomResponseObj.uuid;
                    OracleInvoiceobj.erroeMessage = CustomResponseObj.erroeMessage;
                    OracleInvoiceobj.errorCode = CustomResponseObj.errorCode;
                    OracleInvoiceobj.errorTarget = CustomResponseObj.errorTarget;
                    OracleInvoiceobj.hashKey = CustomResponseObj.hashKey;
                    OracleInvoiceobj.longId = CustomResponseObj.longId;
                    int updateflag = _InvoiceEF.SaveChanges();
                }
                return true;

            }
        }

        public int GetStatus(string invoice)
        {
            int? invoicesttus;
            using (_InvoiceEF = new InvoiceEF())
            {
                var query = _InvoiceEF.OracleInvoices.Where(x => x.internalID == invoice)
                    .Select(m => m.SendStatusId).Distinct();
                invoicesttus = query.FirstOrDefault();
            }
            return int.Parse(invoicesttus.ToString());
        }
        public List<OracleInvoice> GetAllRecords()
        {
            List<OracleInvoice> _OracleInvoice;
            using (_InvoiceEF = new InvoiceEF())
            {
                _OracleInvoice = _InvoiceEF.OracleInvoices.ToList();
            }
            return _OracleInvoice;
        }

        public List<OracleInvoice> GetAllAccecptedRecords()
        {
            List<OracleInvoice> _OracleInvoice = new List<OracleInvoice>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var quary = _InvoiceEF.OracleInvoices.Where(x => x.SendStatusId == 2)
                .Select(m => new
                {
                    m.documentType,
                    m.documentTypeVersion,
                    m.dateTimeIssued,
                    m.taxpayerActivityCode,
                    m.internalID,
                    m.totalDiscountAmount,
                    m.totalSalesAmount,
                    m.netAmount,
                    m.totalAmount,
                    m.extraDiscountAmount,
                    m.totalItemsDiscountAmount,
                    m.uuid,
                    m.longId
                }).Distinct().ToList();
                foreach (var obj in quary)
                {
                    OracleInvoice DocumentObj = new OracleInvoice();
                    DocumentObj.documentType = obj.documentType;
                    if (obj.documentTypeVersion != null)
                        DocumentObj.documentTypeVersion = obj.documentTypeVersion.ToString();
                    if (obj.dateTimeIssued != null)
                        DocumentObj.dateTimeIssued = obj.dateTimeIssued;
                    //  DocumentObj.dateTimeIssued = (DateTime.Parse(obj.dateTimeIssued.ToString())).ToString("s") + "Z";
                    DocumentObj.taxpayerActivityCode = obj.taxpayerActivityCode;
                    DocumentObj.internalID = obj.internalID;
                    if (obj.totalDiscountAmount != null)
                        DocumentObj.totalDiscountAmount = decimal.Parse(obj.totalDiscountAmount.ToString());
                    if (obj.totalSalesAmount != null)
                        DocumentObj.totalSalesAmount = decimal.Parse(obj.totalSalesAmount.ToString());
                    if (obj.netAmount != null)
                        DocumentObj.netAmount = decimal.Parse(obj.netAmount.ToString());
                    if (obj.totalAmount != null)
                        DocumentObj.totalAmount = decimal.Parse(obj.totalAmount.ToString());
                    if (obj.extraDiscountAmount != null)
                        DocumentObj.extraDiscountAmount = decimal.Parse(obj.extraDiscountAmount.ToString());
                    if (obj.totalItemsDiscountAmount != null)
                        DocumentObj.totalItemsDiscountAmount = decimal.Parse(obj.totalItemsDiscountAmount.ToString());
                    DocumentObj.uuid = obj.uuid;
                    _OracleInvoice.Add(DocumentObj);


                }
            }
            return _OracleInvoice;
        }
        public List<string> GetinvoiceNo()
        {
            List<string> _OracleInvoice;
            using (_InvoiceEF = new InvoiceEF())
            {
                var query = _InvoiceEF.OracleInvoices.Where(x => x.SendStatusId == 1 || x.SendStatusId == 3)
                    .Select(m => m.internalID).Distinct();
                _OracleInvoice = query.ToList();
            }
            return _OracleInvoice;
        }
        public Document getDocument(string invoice)
        {
            Document DocumentObj = new Document();
            OracleInvoice _OracleInvoice = new OracleInvoice();

            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                   .Where(x => x.internalID == invoice)
                   .Select(m => new
                   {
                       m.documentType,
                       m.documentTypeVersion,
                       m.dateTimeIssued,
                       m.taxpayerActivityCode,
                       m.internalID,
                       // m.purchaseOrderReference,
                       // m.purchaseOrderDescription,
                       //  m.salesOrderReference,
                       //  m.salesOrderDescription,
                       //  m.totalSales,
                       m.totalDiscountAmount,
                       m.totalSalesAmount,
                       m.netAmount,
                       m.totalAmount,
                       m.extraDiscountAmount,
                       m.totalItemsDiscountAmount
                   }).Distinct().FirstOrDefault();
                DocumentObj.documentType = obj.documentType;
                if (obj.documentTypeVersion != null)
                    DocumentObj.documentTypeVersion = obj.documentTypeVersion.ToString();
                if (obj.dateTimeIssued != null)
                    DocumentObj.dateTimeIssued = (DateTime.Parse(obj.dateTimeIssued.ToString())).ToString("s") + "Z";
                DocumentObj.taxpayerActivityCode = obj.taxpayerActivityCode;
                DocumentObj.internalID = obj.internalID;
                if (obj.totalDiscountAmount != null)
                    DocumentObj.totalDiscountAmount = decimal.Parse(obj.totalDiscountAmount.ToString());
                if (obj.totalSalesAmount != null)
                    DocumentObj.totalSalesAmount = decimal.Parse(obj.totalSalesAmount.ToString());
                if (obj.netAmount != null)
                    DocumentObj.netAmount = decimal.Parse(obj.netAmount.ToString());
                if (obj.totalAmount != null)
                    DocumentObj.totalAmount = decimal.Parse(obj.totalAmount.ToString());
                if (obj.extraDiscountAmount != null)
                    DocumentObj.extraDiscountAmount = decimal.Parse(obj.extraDiscountAmount.ToString());
                if (obj.totalItemsDiscountAmount != null)
                    DocumentObj.totalItemsDiscountAmount = decimal.Parse(obj.totalItemsDiscountAmount.ToString());
                DocumentObj.issuer = GetIssure(invoice);
                DocumentObj.receiver = GetReceiver(invoice);
                // DocumentObj.payment = GetPayment(invoice);
                // DocumentObj.delivery = GetDelivery(invoice);
                DocumentObj.invoiceLines = GetInvoiceLine(invoice);
                DocumentObj.taxTotals = GetTaxTotal(invoice);
                // DocumentObj.Signature=GetSignature(invoice);


            }
            return DocumentObj;
        }
        private Issuer GetIssure(string invoice)
        {
            Issuer IssureObj = new Issuer();

            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                   .Where(x => x.internalID == invoice)
                   .Select(m => new { m.issuer__id, m.issuer__name, m.issuer__type }).Distinct().FirstOrDefault();


                IssureObj.id = obj.issuer__id.ToString();
                IssureObj.name = obj.issuer__name;
                IssureObj.type = obj.issuer__type;
                IssureObj.address = GetIssureAddress(obj.issuer__id, obj.issuer__name, obj.issuer__type);


            }
            return IssureObj;
        }
        private Receiver GetReceiver(string invoice)
        {
            Receiver ReceiverObj = new Receiver();

            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                   .Where(x => x.internalID == invoice)
                   .Select(m => new { m.receiver__id, m.receiver__name, m.receiver__type }).Distinct().FirstOrDefault();

                ReceiverObj.type = obj.receiver__type;
                ReceiverObj.name = obj.receiver__name;
                ReceiverObj.id = obj.receiver__id.ToString();
                ReceiverObj.address = GetReceiverAddress(obj.receiver__id, obj.receiver__name, obj.receiver__type);


            }
            return ReceiverObj;
        }
        private Address GetIssureAddress(string issuer_id, string issuer_name, string issuer_type)
        {
            Address IssureObj = new Address();

            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                   .Where(x => x.issuer__name == issuer_name && x.issuer__type == issuer_type && x.issuer__id == issuer_id)
                   .Select(m => new
                   {
                       // m.issuer__address__additionalInformation,
                       m.issuer__address__branchID,
                       m.issuer__address__buildingNumber,
                       m.issuer__address__country,
                       //  m.issuer__address__floor,
                       m.issuer__address__governate,
                       //  m.issuer__address__landmark,
                       //   m.issuer__address__postalCode,
                       m.issuer__address__regionCity,
                       //   m.issuer__address__room,
                       m.issuer__address__street
                   }).Distinct().FirstOrDefault();


                // IssureObj.additionalInformation = obj.issuer__address__branchID;
                IssureObj.branchID = obj.issuer__address__branchID;
                IssureObj.buildingNumber = obj.issuer__address__buildingNumber;
                IssureObj.country = obj.issuer__address__country;
                //  IssureObj.floor = obj.issuer__address__floor;
                IssureObj.governate = obj.issuer__address__governate;
                //   IssureObj.landmark = obj.issuer__address__landmark;
                //   IssureObj.postalCode = obj.issuer__address__postalCode;
                IssureObj.regionCity = obj.issuer__address__regionCity;
                //   IssureObj.room = obj.issuer__address__room;
                IssureObj.street = obj.issuer__address__street;

            }
            return IssureObj;
        }
        private Address GetReceiverAddress(string Receiver_id, string Receiver_name, string Receiver_type)
        {
            Address IssureObj = new Address();

            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                   .Where(x => x.receiver__name == Receiver_name && x.receiver__type == Receiver_type && x.receiver__id == Receiver_id)
                   .Select(m => new
                   {
                       // m.issuer__address__additionalInformation,
                       m.issuer__address__branchID,
                       m.issuer__address__buildingNumber,
                       m.issuer__address__country,
                       //  m.issuer__address__floor,
                       m.issuer__address__governate,
                       //  m.issuer__address__landmark,
                       //  m.issuer__address__postalCode,
                       m.issuer__address__regionCity,
                       //  m.issuer__address__room,
                       m.issuer__address__street
                   }).Distinct().FirstOrDefault();


                //IssureObj.additionalInformation = obj.issuer__address__branchID;
                //IssureObj.branchID = obj.issuer__address__branchID;
                IssureObj.buildingNumber = obj.issuer__address__buildingNumber;
                IssureObj.country = obj.issuer__address__country;
                //IssureObj.floor = obj.issuer__address__floor;
                IssureObj.governate = obj.issuer__address__governate;
                //IssureObj.landmark = obj.issuer__address__landmark;
                //IssureObj.postalCode = obj.issuer__address__postalCode;
                IssureObj.regionCity = obj.issuer__address__regionCity;
                //IssureObj.room = obj.issuer__address__room;
                IssureObj.street = obj.issuer__address__street;

            }
            return IssureObj;
        }
        //private Payment GetPayment(string invoice)
        //{
        //    Payment Paymentobj = new Payment();
        //    using (_InvoiceEF = new InvoiceEF())
        //    {
        //        var obj = _InvoiceEF.OracleInvoices
        //           .Where(x => x.internalID == invoice)
        //           .Select(m => new { //m.payment__bankName, m.payment__bankAddress
        //           //, m.payment__swiftCode
        //         //  , m.payment__bankAccountIBAN
        //         //  , m.payment__bankAccountNo
        //           , m.payment__terms }).Distinct().FirstOrDefault();

        //        Paymentobj.bankAccountIBAN = obj.payment__bankAccountNo;
        //        Paymentobj.bankAccountNo = obj.payment__bankAccountNo;
        //        Paymentobj.bankName = obj.payment__bankName;
        //        Paymentobj.bankAddress = obj.payment__bankAddress;
        //        Paymentobj.swiftCode = obj.payment__swiftCode;
        //        Paymentobj.terms = obj.payment__terms;

        //    }

        //    return Paymentobj;
        //}
        //private Delivery GetDelivery(string invoice)
        //{
        //    Delivery DeliveryObj = new Delivery();
        //    using (_InvoiceEF = new InvoiceEF())
        //    {
        //        var obj = _InvoiceEF.OracleInvoices
        //         .Where(x => x.internalID == invoice)
        //         .Select(m => new
        //         {
        //             m.delivery__approach,
        //             m.delivery__countryOfOrigin,
        //             m.delivery__dateValidity,
        //             m.delivery__exportPort,
        //             m.delivery__grossWeight,
        //             m.delivery__netWeight,
        //             m.delivery__packaging,
        //             m.delivery__terms
        //         }).Distinct().FirstOrDefault();

        //        DeliveryObj.approach = obj.delivery__approach;
        //        DeliveryObj.countryOfOrigin = obj.delivery__countryOfOrigin;
        //        if (obj.delivery__dateValidity != null)
        //        DeliveryObj.dateValidity = DateTime.Parse(obj.delivery__dateValidity);
        //        DeliveryObj.exportPort = obj.delivery__exportPort;
        //        DeliveryObj.grossWeight = obj.delivery__grossWeight;
        //        DeliveryObj.netWeight = obj.delivery__netWeight;
        //        DeliveryObj.packaging = obj.delivery__packaging;
        //        DeliveryObj.terms = obj.delivery__terms;
        //    }
        //    return DeliveryObj;
        //}
        private List<InvoiceLine> GetInvoiceLine(string invoice)
        {
            List<InvoiceLine> InvoiceLineList = new List<InvoiceLine>();
            Delivery DeliveryObj = new Delivery();
            using (_InvoiceEF = new InvoiceEF())
            {
                var quary = _InvoiceEF.OracleInvoices
                 .Where(x => x.internalID == invoice)
                 .Select(m => new
                 {
                     m.invoiceLines__description,
                     m.invoiceLines__internalCode,
                     m.invoiceLines__itemCode,
                     m.invoiceLines__itemsDiscount,
                     m.invoiceLines__itemType,
                     m.invoiceLines__totalTaxableFees,
                     m.invoiceLines__netTotal,
                     m.invoiceLines__quantity,
                     m.invoiceLines__salesTotal,
                     m.invoiceLines__total,
                     m.invoiceLines__unitType,
                     m.invoiceLines__valueDifference
                 }).Distinct().ToList();

                foreach (var OBJ in quary)
                {
                    InvoiceLine InvoiceLineobj = new InvoiceLine();
                    InvoiceLineobj.description = OBJ.invoiceLines__description;
                    InvoiceLineobj.internalCode = OBJ.invoiceLines__internalCode;
                    if (OBJ.invoiceLines__itemCode != null)
                        InvoiceLineobj.itemCode = OBJ.invoiceLines__itemCode.ToString();
                    if (OBJ.invoiceLines__itemsDiscount != null)
                        InvoiceLineobj.itemsDiscount = decimal.Parse(OBJ.invoiceLines__itemsDiscount.ToString());
                    InvoiceLineobj.itemType = OBJ.invoiceLines__itemType;
                    if (OBJ.invoiceLines__totalTaxableFees != null)
                        InvoiceLineobj.totalTaxableFees = decimal.Parse(OBJ.invoiceLines__totalTaxableFees.ToString());
                    InvoiceLineobj.itemType = OBJ.invoiceLines__itemType;
                    if (OBJ.invoiceLines__netTotal != null)
                        InvoiceLineobj.netTotal = decimal.Parse(OBJ.invoiceLines__netTotal.ToString());
                    if (OBJ.invoiceLines__quantity != null)
                        InvoiceLineobj.quantity = decimal.Parse(OBJ.invoiceLines__quantity.ToString());
                    if (OBJ.invoiceLines__salesTotal != null)
                        InvoiceLineobj.salesTotal = decimal.Parse(OBJ.invoiceLines__salesTotal.ToString());
                    if (OBJ.invoiceLines__total != null)
                        InvoiceLineobj.total = decimal.Parse(OBJ.invoiceLines__total.ToString());
                    InvoiceLineobj.unitType = OBJ.invoiceLines__unitType;
                    if (OBJ.invoiceLines__valueDifference != null)
                        InvoiceLineobj.valueDifference = decimal.Parse(OBJ.invoiceLines__valueDifference.ToString());
                    InvoiceLineobj.unitValue = GetUnitValue(invoice, OBJ.invoiceLines__itemCode, OBJ.invoiceLines__unitType, OBJ.invoiceLines__internalCode, OBJ.invoiceLines__quantity);
                    InvoiceLineobj.discount = GetDiscount(invoice, OBJ.invoiceLines__itemCode, OBJ.invoiceLines__unitType, OBJ.invoiceLines__internalCode, OBJ.invoiceLines__quantity);
                    InvoiceLineobj.taxableItems = GetTaxableItem(invoice, OBJ.invoiceLines__itemCode, OBJ.invoiceLines__unitType, OBJ.invoiceLines__internalCode, OBJ.invoiceLines__quantity);
                    InvoiceLineList.Add(InvoiceLineobj);
                }

            }

            return InvoiceLineList;
        }
        private UnitValue GetUnitValue(string invoice, string itemCode, string unitType, string internalCode, decimal? quantity)
        {
            UnitValue UnitValueObj = new UnitValue();
            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                 .Where(x => x.internalID == invoice
                 && x.invoiceLines__itemCode == itemCode
                 && x.invoiceLines__unitType == unitType
                 && x.invoiceLines__internalCode == internalCode
                 && x.invoiceLines__quantity == quantity)

                 .Select(m => new
                 {
                     m.invoiceLines__unitValue__amountEGP,
                     //   m.invoiceLines__unitValue__amountSold,
                     //  m.invoiceLines__unitValue__currencyExchangeRate,
                     m.invoiceLines__unitValue__currencySold
                 }).Distinct().FirstOrDefault();
                if (obj.invoiceLines__unitValue__amountEGP != null)
                    UnitValueObj.amountEGP = decimal.Parse(obj.invoiceLines__unitValue__amountEGP.ToString());
                // UnitValueObj.amountSold = decimal.Parse(obj.invoiceLines__unitValue__amountSold.ToString());
                // UnitValueObj.currencyExchangeRate =obj.invoiceLines__unitValue__currencyExchangeRate;
                UnitValueObj.currencySold = obj.invoiceLines__unitValue__currencySold;
            }
            return UnitValueObj;
        }
        //private FactoryUnitValue GetFactoryUnitValue(string invoice, string itemCode, string unitType, string internalCode, decimal? quantity)
        //{
        //    FactoryUnitValue FactoryUnitValueObj = new FactoryUnitValue();
        //    using (_InvoiceEF = new InvoiceEF())
        //    {
        //        var obj = _InvoiceEF.OracleInvoices
        //         .Where(x => x.internalID == invoice
        //         && x.invoiceLines__itemCode == itemCode
        //         && x.invoiceLines__unitType == unitType
        //         && x.invoiceLines__internalCode == internalCode
        //         && x.invoiceLines__quantity == quantity)

        //         .Select(m => new
        //         {
        //             m.invoiceLines__factoryUnitValue__amountEGP,
        //             m.invoiceLines__factoryUnitValue__amountSold,
        //             m.invoiceLines__factoryUnitValue__currencyExchangeRate,
        //             m.invoiceLines__factoryUnitValue__currencySold
        //         }).Distinct().FirstOrDefault();
        //        FactoryUnitValueObj.amountEGP = obj.invoiceLines__factoryUnitValue__amountEGP;
        //        FactoryUnitValueObj.amountSold = obj.invoiceLines__factoryUnitValue__amountSold;
        //        FactoryUnitValueObj.currencyExchangeRate = obj.invoiceLines__factoryUnitValue__currencyExchangeRate;
        //        FactoryUnitValueObj.currencySold = obj.invoiceLines__factoryUnitValue__currencySold;
        //    }
        //    return FactoryUnitValueObj;
        //}
        private Discount GetDiscount(string invoice, string itemCode, string unitType, string internalCode, decimal? quantity)
        {
            Discount DiscountObj = new Discount();
            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.OracleInvoices
                 .Where(x => x.internalID == invoice
                 && x.invoiceLines__itemCode == itemCode
                 && x.invoiceLines__unitType == unitType
                 && x.invoiceLines__internalCode == internalCode
                 && x.invoiceLines__quantity == quantity)

                 .Select(m => new
                 {
                     m.invoiceLines__discount__amount,
                     m.invoiceLines__discount__rate
                 }).Distinct().FirstOrDefault();
                if (obj.invoiceLines__discount__amount != null)
                    DiscountObj.amount = decimal.Parse(obj.invoiceLines__discount__amount.ToString());
                if (obj.invoiceLines__discount__rate != null)
                    DiscountObj.rate = decimal.Parse(obj.invoiceLines__discount__rate.ToString());
            }
            return DiscountObj;
        }

        private List<TaxableItem> GetTaxableItem(string invoice, string itemCode, string unitType, string internalCode, decimal? quantity)
        {
            List<TaxableItem> TaxableItemList = new List<TaxableItem>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var Quary = _InvoiceEF.OracleInvoices
                 .Where(x => x.internalID == invoice
                 && x.invoiceLines__itemCode == itemCode
                 && x.invoiceLines__unitType == unitType
                 && x.invoiceLines__internalCode == internalCode
                 && x.invoiceLines__quantity == quantity)

                 .Select(m => new
                 {
                     m.invoiceLines__taxableItems__amount,
                     m.invoiceLines__taxableItems__rate,
                     m.invoiceLines__taxableItems__subType,
                     m.invoiceLines__taxableItems__taxType
                 }).Distinct().ToList();
                foreach (var obj in Quary)
                {
                    TaxableItem TaxableItemObj = new OracleInvoiceDB.TaxableItem();
                    if (obj.invoiceLines__taxableItems__amount != null)
                        TaxableItemObj.amount = decimal.Parse(obj.invoiceLines__taxableItems__amount.ToString());
                    if (obj.invoiceLines__taxableItems__rate != null)
                        TaxableItemObj.rate = decimal.Parse(obj.invoiceLines__taxableItems__rate.ToString());
                    TaxableItemObj.subType = obj.invoiceLines__taxableItems__subType;
                    TaxableItemObj.taxType = obj.invoiceLines__taxableItems__taxType;
                    TaxableItemList.Add(TaxableItemObj);
                }
            }
            return TaxableItemList;
        }
        private List<TaxTotal> GetTaxTotal(string invoice)
        {
            List<TaxTotal> TaxTotalList = new List<TaxTotal>();
            Delivery DeliveryObj = new Delivery();
            using (_InvoiceEF = new InvoiceEF())
            {
                var quary = _InvoiceEF.OracleInvoices
                 .Where(x => x.invoice_id == invoice)
                 .Select(m => new
                 {
                     m.taxTotals__amount,
                     m.taxTotals__taxType
                 }).Distinct().ToList();
                foreach (var obj in quary)
                {
                    TaxTotal TaxTotalObj = new TaxTotal();
                    if (obj.taxTotals__amount != null)
                        TaxTotalObj.amount = decimal.Parse(obj.taxTotals__amount.ToString());
                    TaxTotalObj.taxType = obj.taxTotals__taxType;
                    TaxTotalList.Add(TaxTotalObj);
                }
                return TaxTotalList;
            }
        }
        private List<Signature> GetSignature(string invoice)
        {
            List<Signature> SignatureList = new List<Signature>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var quary = _InvoiceEF.OracleInvoices
                 .Where(x => x.internalID == invoice)
                 .Select(m => new
                 {
                     m.signatureType,
                     m.signaturevalue
                 }).Distinct().ToList();
                foreach (var obj in quary)
                {
                    Signature SignatureObj = new Signature();
                    SignatureObj.Type = obj.signatureType;
                    SignatureObj.value = obj.signaturevalue;
                    SignatureList.Add(SignatureObj);
                }
                return SignatureList;
            }
        }

        private string DisplayDateWithTimeZoneName(DateTime date1, TimeZoneInfo timeZone)
        {
            string datezone = timeZone.IsDaylightSavingTime(date1) ? timeZone.DaylightName : timeZone.StandardName;
            return datezone;
        }
    }
}
