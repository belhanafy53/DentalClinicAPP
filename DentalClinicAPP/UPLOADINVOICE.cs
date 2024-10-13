using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class UploadInvoice
    {
        private InvoiceEF _InvoiceEF;

        public List<document> GetAllRecords()
        {
            List<document> _document;
            using (_InvoiceEF = new InvoiceEF())
            {
                _document = _InvoiceEF.documents
                    .Include("Issuer")
                    .Include("Receiver").ToList();
            }
            return _document;
        }
    }
}
