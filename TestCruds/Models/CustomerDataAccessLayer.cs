using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestCruds.Models
{
    public class CustomerDataAccessLayer
    {
        TestDetailContext db = new TestDetailContext();

        //public object APIHelperMethods { get; private set; }

        public IEnumerable<CustomerTbl> GetAllCustomer()
        {
            try
            {
                return db.CustomerTbl.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int AddCustomer(CustomerTbl cust)
        {
            try
            {
                db.CustomerTbl.Add(cust);
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int UpdateCustomer(CustomerTbl cust)
        {
            try
            {
                db.Entry(cust).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustomerTbl GetCustomer(int id)
        {
            try
            {
                CustomerTbl c = db.CustomerTbl.Find(id);
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteCustomer(int id)
        {
            try
            {
                CustomerTbl cust = db.CustomerTbl.Find(id);
                db.CustomerTbl.Remove(cust);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<InvoiceTbl> GetAllInvoice()
        {
            try
            {
                return db.InvoiceTbl.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddInvoice(InvoiceTbl inv)
        {
            try
            {
                db.InvoiceTbl.Add(inv);
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public int UpdateInvoice(InvoiceTbl inv)
        {
            try
            {
                db.Entry(inv).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public InvoiceTbl GetInvoice(int id)
        {
            try
            {
                InvoiceTbl c = db.InvoiceTbl.Find(id);
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteInvoice(int id)
        {
            try
            {
                InvoiceTbl inv = db.InvoiceTbl.Find(id);
                db.InvoiceTbl.Remove(inv);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<PaymentTbl> GetAllPayment()
        {
            try
            {
                return db.PaymentTbl.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int AddPayment(PaymentTbl pt)
        {
            try
            {
                db.PaymentTbl.Add(pt);
                db.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int UpdatePayment(PaymentTbl pt)
        {
            try
            {
                db.Entry(pt).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PaymentTbl GetPayment(int id)
        {
            try
            {
                PaymentTbl c = db.PaymentTbl.Find(id);
                return c;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeletePayment(int id)
        {
            try
            {
                PaymentTbl inv = db.PaymentTbl.Find(id);
                db.PaymentTbl.Remove(inv);
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetAllInvoiceDetails(int InvoiceID, string CustomerName, DateTime InvoiceDate, string InvoiceAmount, string PaymentAmount)
        {
            try
            {
                using (var context = new TestDetailContext() )
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "proc_GetAllInvoiceDetail";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@InvoiceID", InvoiceID));
                    command.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                    command.Parameters.Add(new SqlParameter("@InvoiceDate", InvoiceDate));
                    command.Parameters.Add(new SqlParameter("@InvoiceAmount", InvoiceAmount));
                    command.Parameters.Add(new SqlParameter("@PaymentAmount", PaymentAmount));
                    //DbDataAdapter da = APIHelperMethods.CreateDataAdapter(command);
                    DbDataAdapter adapter = DbProviderFactories.GetFactory(command.Connection).CreateDataAdapter();
                    adapter.SelectCommand = command;
                    DataSet result = new DataSet();
                    adapter.Fill(result);
                    string JSONString = string.Empty;
                    JSONString = JsonConvert.SerializeObject(result);
                    return JSONString;
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return null;
            }
        }

    }
}
