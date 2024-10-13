using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public class CustomResponse
    {
        public string internalId { get; set; }
        public string submissionID { get; set; }
        public string longId { get; set; }
        public string hashKey { get; set; }
        public string errorCode { get; set; }
        public string erroeMessage { get; set; }
        public string errorTarget { get; set; }
        public string uuid { get; set; }

        public string errordetailsmessage { get; set; }

        public string errordetailsTargt { get; set; }

       
    }
}
