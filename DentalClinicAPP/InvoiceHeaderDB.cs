
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DentalClinicAPP
{
    public class InvoiceHeaderDB
    {
        private InvoiceEF _InvoiceEF;

        public int SaveRecord(InvoiceHeader record)
        {
            record.InsertDate = DateTime.Now;
            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceEF.InvoiceHeaders.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.ID;
        }

        public bool UpdateRecord(InvoiceHeader record)
        {

            InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderObj = _InvoiceEF.InvoiceHeaders.Where(x => x.ID == record.ID).FirstOrDefault();

                InvoiceHeaderObj.dateTimeIssued = record.dateTimeIssued;
                InvoiceHeaderObj.documentType = record.documentType;
                InvoiceHeaderObj.documentTypeVersion = record.documentTypeVersion;
                InvoiceHeaderObj.erroeMessage = record.erroeMessage;
                InvoiceHeaderObj.errorCode = record.errorCode;
                InvoiceHeaderObj.errordetailsmessage = record.errordetailsmessage;
                InvoiceHeaderObj.errordetailsTargt = record.errordetailsTargt;
                InvoiceHeaderObj.errorTarget = record.errorTarget;
                InvoiceHeaderObj.extraDiscountAmount = record.extraDiscountAmount;
                InvoiceHeaderObj.hashKey = record.hashKey;
                InvoiceHeaderObj.internalID = record.internalID;
                InvoiceHeaderObj.issuer_branchID = record.issuer_branchID;
                InvoiceHeaderObj.issuer_buildingNumber = record.issuer_buildingNumber;
                InvoiceHeaderObj.issuer_country = record.issuer_country;
                InvoiceHeaderObj.issuer_governate = record.issuer_governate;
                InvoiceHeaderObj.issuer_id = record.issuer_id;
                InvoiceHeaderObj.issuer_name = record.issuer_name;
                InvoiceHeaderObj.issuer_regionCity = record.issuer_regionCity;
                InvoiceHeaderObj.issuer_street = record.issuer_street;
                InvoiceHeaderObj.issuer_type = record.issuer_type;
                InvoiceHeaderObj.longId = record.longId;
                InvoiceHeaderObj.netAmount = record.netAmount;
                InvoiceHeaderObj.receiver_buildingNumber = record.receiver_buildingNumber;
                InvoiceHeaderObj.receiver_country = record.receiver_country;
                InvoiceHeaderObj.receiver_governate = record.receiver_governate;
                InvoiceHeaderObj.receiver_id = record.receiver_id;
                InvoiceHeaderObj.receiver_name = record.receiver_name;
                InvoiceHeaderObj.receiver_regionCity = record.receiver_regionCity;
                InvoiceHeaderObj.receiver_street = record.receiver_street;
                InvoiceHeaderObj.receiver_type = record.receiver_type;
                //InvoiceHeaderObj.SendStatu = record.SendStatu;
                InvoiceHeaderObj.SendStatusId = record.SendStatusId;
                InvoiceHeaderObj.signatureType = record.signatureType;
                InvoiceHeaderObj.signaturevalue = record.signaturevalue;
                InvoiceHeaderObj.submissionID = record.submissionID;
                InvoiceHeaderObj.taxpayerActivityCode = record.taxpayerActivityCode;
                InvoiceHeaderObj.taxTotals__amount = record.taxTotals__amount;
                InvoiceHeaderObj.taxTotals__taxType = record.taxTotals__taxType;
                InvoiceHeaderObj.totalAmount = record.totalAmount;
                InvoiceHeaderObj.totalDiscountAmount = record.totalDiscountAmount;
                InvoiceHeaderObj.totalItemsDiscountAmount = record.totalItemsDiscountAmount;
                InvoiceHeaderObj.totalSalesAmount = record.totalSalesAmount;

                InvoiceHeaderObj.UpdateBy = record.UpdateBy;
                InvoiceHeaderObj.UpdateDate = record.UpdateDate;
                InvoiceHeaderObj.uuid = record.uuid;
                int updateflag = _InvoiceEF.SaveChanges();

                if (updateflag > 0)
                    return true;
                else
                    return false;
            }



        }


        public bool DeleteRecord(InvoiceHeader record)
        {

            InvoiceHeader _OracleInvoiceObj = new InvoiceHeader();
            using (_InvoiceEF = new InvoiceEF())
            {
                _OracleInvoiceObj = _InvoiceEF.InvoiceHeaders.Where(x => x.ID == record.ID).FirstOrDefault();

                _InvoiceEF.InvoiceHeaders.Remove(_OracleInvoiceObj);
                int deleteflag = _InvoiceEF.SaveChanges();
                if (deleteflag > 0)
                    return true;
                else
                    return false;
            }


        }

        public List<InvoiceHeader> GetAll()
        {
            List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderList = _InvoiceEF.InvoiceHeaders.OrderByDescending(x => x.InsertDate).ToList();
            }
            return InvoiceHeaderList;
        }

        public List<InvoiceHeader> GetAll(int SendId)

        {
            List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();

            using (_InvoiceEF = new InvoiceEF())
            {
                if (SendId == 0)
                    InvoiceHeaderList = _InvoiceEF.InvoiceHeaders.OrderByDescending(x => x.InsertDate).ToList();
                else
                    InvoiceHeaderList = _InvoiceEF.InvoiceHeaders.Where(x => x.SendStatusId == SendId).OrderByDescending(x => x.InsertDate).ToList();
            }
            return InvoiceHeaderList;
        }

        public List<InvoiceHeader> GetAll(int SendId, int workerid)

        {
            List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();

            using (_InvoiceEF = new InvoiceEF())
            {
                if (SendId == 0)
                    InvoiceHeaderList = _InvoiceEF.InvoiceHeaders.Where(x => x.InsertedBy == workerid).OrderByDescending(x => x.InsertDate).ToList();
                else
                    InvoiceHeaderList = _InvoiceEF.InvoiceHeaders.Where(x => x.SendStatusId == SendId && x.InsertedBy == workerid).OrderByDescending(x => x.InsertDate).ToList();
            }
            return InvoiceHeaderList;
        }

        public List<InvoiceHeader> GetAllNotSend()
        {
            List<InvoiceHeader> InvoiceHeaderList = new List<InvoiceHeader>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderList = _InvoiceEF.InvoiceHeaders
                    .Where(x => x.SendStatusId == 1).ToList();

            }
            return InvoiceHeaderList;
        }

        public InvoiceHeader GetRecordById(int Id)
        {
            InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderObj = _InvoiceEF.InvoiceHeaders.Where(x => x.ID == Id).FirstOrDefault();
            }
            return InvoiceHeaderObj;
        }

        public InvoiceHeader GetRecordByInternalID(string InternalID)
        {
            InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderObj = _InvoiceEF.InvoiceHeaders.Where(x => x.internalID == InternalID).FirstOrDefault();
            }
            return InvoiceHeaderObj;
        }
        public bool UpdateRecordByInternalId(string invoice, int sendstatus, CustomResponse CustomResponseObj)
        {
            int updateflag = 0;
            List<InvoiceHeader> _OracleInvoiceList = new List<InvoiceHeader>();
            using (_InvoiceEF = new InvoiceEF())
            {
                var obj = _InvoiceEF.InvoiceHeaders.Where(x => x.internalID == invoice);
                _OracleInvoiceList = obj.ToList();
                foreach (InvoiceHeader OracleInvoiceobj in _OracleInvoiceList)
                {
                    OracleInvoiceobj.SendStatusId = int.Parse(sendstatus.ToString());
                    OracleInvoiceobj.submissionID = CustomResponseObj.submissionID;
                    OracleInvoiceobj.uuid = CustomResponseObj.uuid;
                    OracleInvoiceobj.erroeMessage = CustomResponseObj.erroeMessage;
                    OracleInvoiceobj.errorCode = CustomResponseObj.errorCode;
                    OracleInvoiceobj.errorTarget = CustomResponseObj.errorTarget;
                    OracleInvoiceobj.hashKey = CustomResponseObj.hashKey;
                    OracleInvoiceobj.longId = CustomResponseObj.longId;
                    updateflag = _InvoiceEF.SaveChanges();

                }
                if (updateflag > 0)
                    return true;
                else
                    return false;


            }
        }

        public bool UpdateRecordSendstatus(int Id, int sendstatus)
        {
            int updateflag = 0;

            InvoiceHeader InvoiceHeaderObj = new InvoiceHeader();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderObj = _InvoiceEF.InvoiceHeaders.Where(x => x.ID == Id).FirstOrDefault();


                InvoiceHeaderObj.SendStatusId = int.Parse(sendstatus.ToString());
                updateflag = _InvoiceEF.SaveChanges();
            }

            if (updateflag > 0)
                return true;
            else
                return false;


        }

    }
}
