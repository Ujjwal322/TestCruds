using System;
using System.Collections.Generic;

namespace TestCruds.Models
{
    public partial class PaymentTbl
    {
        public int PaymentId { get; set; }
        public string PaymentNo { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentAmount { get; set; }

        public virtual InvoiceTbl Invoice { get; set; }
    }
}
