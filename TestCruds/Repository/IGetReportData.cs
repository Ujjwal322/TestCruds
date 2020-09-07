using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCruds.Models;

namespace TestCruds.Repository
{
    public interface IGetReportData
    {
        List<Models.Invoice> GetInvoices();
    }
}
