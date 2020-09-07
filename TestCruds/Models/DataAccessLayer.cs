using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TestCruds.Models
{
    public class DataAccessLayer
    {
        string connectionstring = "Data Source=10.61.18.12;Initial Catalog=TestDetail;Persist Security Info=True;User ID=msdba;Password=dba@123";

        //public IEnumerable<Invoice> GetInvoiceList()
        //{
        //    try
        //    {
        //        List<Invoice> lstinvoices = new List<Invoice>();

        //        using (SqlConnection con = new SqlConnection(connectionstring))
        //        {
        //            SqlCommand cmd = new SqlCommand("proc_GetAllInvoiceDetail", con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader();

        //            while (rdr.Read())
        //            {
        //                Invoice inv = new Invoice();

        //                inv.InvoiceID = Convert.ToInt32(rdr["InvoiceID"]);
        //                inv.CustomerName = rdr["CustomerName"].ToString();
        //               // inv.InvoiceDate = Convert.ToDateTime(rdr["InvoiceDate"]);
        //                inv.InvoiceAmount = rdr["InvoiceAmount"].ToString();
        //                inv.PaymentAmount = rdr["PaymentAmount"].ToString();

        //                lstinvoices.Add(inv);
        //            }
        //            con.Close();
        //        }
        //        return lstinvoices;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public List<Invoice> GetInvoiceList()
        {
            DataSet ds = null;
            try
            {
                List<Invoice> lstinvoices = new List<Invoice>();

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("proc_GetAllInvoiceDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", null);

                    cmd.Parameters.AddWithValue("@CustomerName", null);

                    cmd.Parameters.AddWithValue("@InvoiceAmount", null);

                    cmd.Parameters.AddWithValue("@PaymentAmount", null);
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    ds = new DataSet();
                    da.Fill(ds);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Invoice inv = new Invoice();

                        inv.NoOfInvoices = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfInvoices"].ToString());
                        //inv.NoOfInvoices = Convert.ToInt32(ds.Tables[0].Rows[i]["NoOfInvoices"]);
                        inv.CustomerName = ds.Tables[0].Rows[i]["CustomerName"].ToString();
                         inv.Month = ds.Tables[0].Rows[i]["Month"].ToString();
                        inv.Sales = Convert.ToInt32(ds.Tables[0].Rows[i]["Sales"]);
                        inv.PaymentCollection = Convert.ToInt32(ds.Tables[0].Rows[i]["PaymentCollection"]);

                        lstinvoices.Add(inv);
                    }
                    con.Close();
                }
                return lstinvoices;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
