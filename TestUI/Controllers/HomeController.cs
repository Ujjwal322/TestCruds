using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using TestC.Models;
using TestUI.Helper;
using TestUI.Models;
//using TestUI.Models;

namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        CustomerAPI api = new CustomerAPI();


        public async  Task<IActionResult> Index()
        {
            List<Invoice> invoices = new List<Invoice>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Customer/GetInvoicesDetail");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                invoices = JsonConvert.DeserializeObject<List<Invoice>>(result);
            }

            // return View(invoices);
            return Json(new { data = invoices });
        }

        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
