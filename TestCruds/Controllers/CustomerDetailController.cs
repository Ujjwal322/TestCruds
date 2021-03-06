﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCruds.Models;
using TestCruds.Repository;

namespace TestCruds.Controllers
{
    [Route("api/[controller]")]
    public class CustomerDetailController : Controller
    {
        private TestDetailContext _testDetailContext;

        public CustomerDetailController( TestDetailContext testDetailContext)
        {
            _testDetailContext = testDetailContext;
        }

        [HttpGet]
        //public List<Invoice> Get()
        //{
        //    return _testDetailContext.Invoices.ToList();
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}