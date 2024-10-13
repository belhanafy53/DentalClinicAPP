using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class EtaReponceDB
    {
        private InvoiceEF _InvoiceEF;

        public int SaveRecord(EtaReponce record)
        {
            using (_InvoiceEF = new InvoiceEF())
            {
                _InvoiceEF.EtaReponces.Add(record);
                _InvoiceEF.SaveChanges();
            }

            return record.Id;
        }
    }
}
