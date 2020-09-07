using System;
using System.Collections.Generic;

namespace TestCruds.Models
{
    public partial class CustomerTbl
    {
        public CustomerTbl()
        {
            InvoiceTbl = new HashSet<InvoiceTbl>();
        }

        public int CustomerId { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<InvoiceTbl> InvoiceTbl { get; set; }
    }
}
