using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestCruds.Models
{
    public partial class CustomerTbl
    {
        public CustomerTbl()
        {
            InvoiceTbl = new HashSet<InvoiceTbl>();
        }

        public int CustomerId { get; set; }
        [Required]
        public string CustomerNo { get; set; }
        [Required]
        public string CustomerName { get; set; }

        public virtual ICollection<InvoiceTbl> InvoiceTbl { get; set; }
    }
}
