using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCruds.Models
{
    public class Invoices
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        //public int CustomerId { get; set; }

        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceAmount { get; set; }
        public DateTime PaymentDueDate { get; set; }

    }
}
