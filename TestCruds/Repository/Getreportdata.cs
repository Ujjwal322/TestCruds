using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCruds.Models;

namespace TestCruds.Repository
{
    public class Getreportdata : IGetReportData
    {
        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                TestDetailContext db = new TestDetailContext();
                List<InvoiceTbl> inv = new List<InvoiceTbl>();
                List<InvoiceTbl> invdata = db.InvoiceTbl.ToList();
                {
                    Models.Invoice Invoices;

                    int i = 0;

                    foreach (var Inv in invdata)
                    {
                        Invoices = new Models.Invoice();

                        var Cust = db.CustomerTbl.FirstOrDefault(x => x.CustomerId == Inv.CustomerId);
                        Invoices.DateOfMonthInvoice = new DateTime(Inv.InvoiceDate.Year, Inv.InvoiceDate.Month, 11);
                        Invoices.DateOfMonth = Invoices.DateOfMonthInvoice;

                        if (Cust != null)
                        {
                            Invoices.CustomerID = Inv.CustomerId;
                            Invoices.CustomerName = Cust.CustomerName;
                        }

                        Invoices.NoOfInvoices = 1;
                        Boolean b = false;


                        foreach (var a in invoices)
                        {
                            if (a.DateOfMonthInvoice == Invoices.DateOfMonthInvoice && a.CustomerID == Invoices.CustomerID)
                            {
                                a.Sales = Convert.ToInt32(Inv.InvoiceAmount) + a.Sales;
                                a.NoOfInvoices = a.NoOfInvoices + 1;
                                b = true;


                            }
                        }

                        if (b == false)
                        {
                            Invoices.Sales = Convert.ToInt32(Inv.InvoiceAmount);
                            i++;
                            // Invoice1.ID = i;
                            invoices.Add(Invoices);
                        }
                    }

                    List<PaymentTbl> payments = new List<PaymentTbl>();
                    List<PaymentTbl> paydata = db.PaymentTbl.ToList();

                    foreach (var Pay in paydata)
                    {
                        Invoices = new Models.Invoice();

                        var Inv = db.InvoiceTbl.FirstOrDefault(x => x.InvoiceId == Pay.InvoiceId);

                        var Cust = db.CustomerTbl.FirstOrDefault(x => x.CustomerId == Inv.CustomerId);

                        if (Pay != null)
                        {
                            Invoices.DateOfMonthPay = new DateTime(Pay.PaymentDate.Year, Pay.PaymentDate.Month, 11);
                            Invoices.DateOfMonth = Invoices.DateOfMonthPay;
                            // Invoice1.Month = Invoice1.DateOfMonth.ToString("MMM-yy");

                        }

                        if (Cust != null)
                        {
                            Invoices.CustomerID = Cust.CustomerId;
                            Invoices.CustomerName = Cust.CustomerName;
                        }
                        Boolean b = false;

                        foreach (var a in invoices)
                        {
                            if ((a.DateOfMonth == Invoices.DateOfMonthPay || a.DateOfMonthPay == Invoices.DateOfMonthPay) && a.CustomerID == Invoices.CustomerID)
                            {
                                if (Pay != null)
                                {
                                    a.PaymentCollection = Convert.ToInt32(Pay.PaymentAmount) + a.PaymentCollection;
                                }
                                b = true;
                            }
                        }
                        if (b == false)
                        {
                            if (Pay != null)
                            {
                                Invoices.PaymentCollection = Convert.ToInt32(Pay.PaymentAmount);
                            }
                            invoices.Add(Invoices);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            invoices = invoices.OrderBy(e => e.DateOfMonth).ThenBy(e => e.CustomerName).ToList();
            return invoices;

        }
    }
}
