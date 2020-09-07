using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestUI.Models
{
    public class Invoice
    {
        public DateTime DateOfMonthInvoice { get; set; }
        public DateTime DateOfMonthPay { get; set; }
        public DateTime DateOfMonth { get; set; }


        public int CustomerID { get; set; }
        public int NoOfInvoices { get; set; }

        public string CustomerName { get; set; }

       // public DateTime? InvoiceDate { get; set; }

            public string Month { get; set; }

        public int Sales { get; set; }

        public int PaymentCollection { get; set; }
    }
}
