﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiDemo.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        [HttpGet()]
        public JsonResult GetCustomers()
        {

            return new JsonResult(Repository.Instance.Customers);

            //return new JsonResult(new List<object>()
            //{
            //    new { CustomerID =1, ContactName = "Anderson"},
            //    new { CustomerID =2, ContactName = "Solaris"}
            //});
        }

        [HttpGet("{id}")]
        //Regresar tipo generico ejemplo de JsonResult  retornar IACtionResult
        public IActionResult GetCustomer(int id)
        {
            var result =
                Repository.Instance.Customers.
                FirstOrDefault(c => c.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
            //return new JsonResult(result);
        }
    }
}
