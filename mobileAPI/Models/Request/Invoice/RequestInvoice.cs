using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mobileAPI.Models.Request.Invoice
{
    public class RequestInvoice
    {
        public string company_id { get; set; }
        public string si_prefix { get; set; }
        public string si_no { get; set; }
        public string si_prefix_no { get; set; }
        public string si_udf_code_id { get; set; }
        public string doucment_id { get; set; }
        public DateTime si_date { get; set; }


    }
}