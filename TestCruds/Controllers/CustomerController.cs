using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCruds.Models;
using System.Data;
using System.Data.SqlClient;
using TestCruds.Repository;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCruds.Controllers
{
    //[Route("api/[controller]")]
    //[EnableCors("AllowMyOrigin")]
    public class CustomerController : Controller
    {
        public bool IsTenantExist { get; set; }
        CustomerDataAccessLayer cust = new CustomerDataAccessLayer();

        DataAccessLayer db = new DataAccessLayer();

        private readonly IGetReportData gr;

        public CustomerController(IGetReportData reportData)
        {
            gr = reportData;
        }


        [HttpGet]
        [Route("api/Customer/Index")]
        public IEnumerable<CustomerTbl> Index()
        {
            return cust.GetAllCustomer();
        }


        [HttpGet]
        [Route("api/Customer/GetCustomerList")]

        public IEnumerable<CustomerTbl> GetCustomerList()
        {
            return cust.GetCustomers();
        }

        [HttpPost]
        [Route("api/Customer/Create")]
        public int Create([FromBody] CustomerTbl c, string CustomerName)
        {
            return cust.AddCustomer(c,CustomerName);
        }

        //public IActionResult Create([FromBody] CustomerTbl c)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    CustomerTbl existingCustomer = TestDetailContext.CustomerTbl.Where(a => a.CustomerName == a.CustomerName).FirstOrDefault();

        //    if(existingCustomer != null)
        //    {
        //        ModelState.AddModelError("customerName", "This name aleady exists");
        //        return BadRequest(ModelState);
        //    }

        //    TestDetailContext.CustomerTbl.Add(c);
        //    TestDetailContext.SaveChanges();

        //    return Json(c);
        //}

        [HttpGet]
        [Route("api/Customer/Details/{id}")]
        public CustomerTbl Details(int id)
        {
            return cust.GetCustomer(id);
        }

        [HttpPut]
        [Route("api/Customer/Edit")]
        public int Edit([FromBody]CustomerTbl c, int Customerid, string Customername)
        {
            return cust.UpdateCustomer(c, Customerid, Customername);
        }

        [HttpDelete]
        [Route("api/Customer/Delete/{id}")]
        public int Delete(int id)
        {
            return cust.DeleteCustomer(id);
        }


        [HttpGet]
        [Route("api/Invoice/GetInvoice")]
        public IEnumerable<InvoiceTbl> GetInvoice()
        {
            return cust.GetAllInvoice();
        }

        [HttpPost]
        [Route("api/Invoice/Create")]
        public int Create([FromBody] InvoiceTbl i)
        {
            return cust.AddInvoice(i);
        }

        [HttpGet]
        [Route("api/Invoice/DetailsInvoice/{id}")]
        public InvoiceTbl DetailsInvoice(int id)
        {
            return cust.GetInvoice(id);
        }


        [HttpPut]
        [Route("api/Invoice/Edit")]
        public int Edit([FromBody]InvoiceTbl i)
        {
            return cust.UpdateInvoice(i);
        }

        [HttpDelete]
        [Route("api/Invoice/DeleteInv/{id}")]
        public int DeleteInv(int id)
        {
            return cust.DeleteInvoice(id);
        }


        [HttpGet]
        [Route("api/Payment/GetPayment")]
        public IEnumerable<PaymentTbl> GetPayment()
        {
            return cust.GetAllPayment();
        }

        [HttpPost]
        [Route("api/Payment/Create")]
        public int Create([FromBody] PaymentTbl i)
        {
            return cust.AddPayment(i);
        }

        [HttpGet]
        [Route("api/Payment/DetailsPayment/{id}")]
        public PaymentTbl DetailsPayment(int id)
        {
            return cust.GetPayment(id);
        }

        [HttpPut]
        [Route("api/Payment/Edit")]
        public int Edit([FromBody]PaymentTbl i)
        {
            return cust.UpdatePayment(i);
        }

        [HttpDelete]
        [Route("api/Payment/DeletePay/{id}")]
        public int DeletePay(int id)
        {
            return cust.DeletePayment(id);
        }

        [HttpGet]
        [Route("api/Customer/GetCustomers")]

        public List<Invoice> GetCustomers()
        {
            return db.GetInvoiceList();
        }


        //[HttpGet("proc_GetAllInvoiceDetail")]

        //public string proc_GetAllInvoiceDetail(int InvoiceId, string CustomerName, DateTime InvoiceDate, string InvoiceAmount, string PaymentAmount)
        //{
        //    if (IsTenantExist)
        //        return cust.GetAllInvoiceDetails(InvoiceId, CustomerName, InvoiceDate, InvoiceAmount, PaymentAmount);
        //}




        [HttpGet]
        [Route("api/Customer/GetInvoicesDetail")]

        public List<Invoice> GetInvoicesDetail()
        {
            return gr.GetInvoices();
        }


    }
}
