using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class ErrorDetailDB
    {
        private InvoiceEF _InvoiceEF;

        public int SaveRecord(ErrorDetail record)
        {
            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceEF.ErrorDetails.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.Id;
        }

        public bool updateRecord(string internalid)
        {
            List<ErrorDetail> ErrorDetailList = new List<ErrorDetail>();
            bool updatefla = false; ;
            using (_InvoiceEF = new InvoiceEF())
            {
                var qury = _InvoiceEF.ErrorDetails.Where(x => x.InternalId == internalid);
                if (qury != null)
                {
                    ErrorDetailList = qury.ToList();
                    foreach (ErrorDetail ErrorDetailObj in ErrorDetailList)
                    {
                        ErrorDetailObj.UpdateFlag = true;
                        int updateflag = _InvoiceEF.SaveChanges();

                        if (updateflag > 0)
                            updatefla = true;
                        else
                            updatefla = false;
                    }
                }

                return updatefla;
            }
        }

        public List<ErrorDetail> GetAllCurrantErrordetail()
        {
            List<ErrorDetail> InvoiceHeaderList = new List<ErrorDetail>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderList = _InvoiceEF.ErrorDetails
                    .Where(x => x.UpdateFlag == null).ToList();

            }
            return InvoiceHeaderList;
        }

        public List<ErrorDetail> GetAllCurrantErrordetail(int workerid)
        {
            List<ErrorDetail> InvoiceHeaderList = new List<ErrorDetail>();
            using (_InvoiceEF = new InvoiceEF())
            {
                InvoiceHeaderList = _InvoiceEF.ErrorDetails
                    .Where(x => x.UpdateFlag == null && x.Insertby == workerid).ToList();

            }
            return InvoiceHeaderList;
        }
    }
}